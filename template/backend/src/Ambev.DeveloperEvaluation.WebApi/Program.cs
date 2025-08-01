using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Starting web application...");
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            Console.WriteLine("WebApplicationBuilder created");
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();

            // Configuração do banco de dados
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Connection String: {connectionString}");
            
            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                )
            );

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ambev Developer Evaluation API", Version = "v1" });
            });

            Console.WriteLine("Configuring AutoMapper...");
            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            Console.WriteLine("Configuring JWT authentication...");
            builder.Services.AddJwtAuthentication(builder.Configuration);
            Console.WriteLine("Registering dependencies...");
            builder.RegisterDependencies();

            Console.WriteLine("Configuring validators...");
            builder.Services.AddValidatorsFromAssembly(typeof(Ambev.DeveloperEvaluation.Application.ApplicationLayer).Assembly);

            Console.WriteLine("Configuring MediatR...");
            builder.Services.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssembly(typeof(Ambev.DeveloperEvaluation.Application.ApplicationLayer).Assembly);
            });
            Console.WriteLine("MediatR configured successfully");

            Console.WriteLine("Configuring validation behavior...");
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Verificar se o MediatR está registrado
            var mediatorService = builder.Services.FirstOrDefault(s => s.ServiceType == typeof(MediatR.IMediator));
            Console.WriteLine($"MediatR service found: {mediatorService != null}");

            Console.WriteLine("Building application...");
            WebApplication app;
            try
            {
                app = builder.Build();
                Console.WriteLine("Application built successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error building application: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
            
            Console.WriteLine("Configuring middleware...");

            Console.WriteLine("Configuring middleware...");
            app.UseMiddleware<ValidationExceptionMiddleware>();

            // Configuração do Swagger
            Console.WriteLine("Configuring Swagger...");
            app.UseSwagger();
            app.UseSwaggerUI();

            // Redirecionar a raiz para o Swagger UI
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

            Console.WriteLine("Configuring authentication and authorization...");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseBasicHealthChecks();
            app.MapControllers();

            Console.WriteLine("Starting application...");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
