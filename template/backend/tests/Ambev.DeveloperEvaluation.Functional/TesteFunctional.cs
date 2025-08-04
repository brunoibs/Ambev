using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.ORM;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;

namespace Ambev.DeveloperEvaluation.Functional;

/// <summary>
/// Classe base para testes funcionais da API
/// </summary>
public abstract class FunctionalTestBase : IClassFixture<CustomWebApplicationFactory<Program>>
{
    protected readonly CustomWebApplicationFactory<Program> _factory;
    protected readonly HttpClient _client;
    protected readonly JsonSerializerOptions _jsonOptions;

    protected FunctionalTestBase(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    /// <summary>
    /// Helper para fazer requisições POST
    /// </summary>
    protected async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
    {
        var json = JsonSerializer.Serialize(data, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return await _client.PostAsync(endpoint, content);
    }

    /// <summary>
    /// Helper para fazer requisições GET
    /// </summary>
    protected async Task<HttpResponseMessage> GetAsync(string endpoint)
    {
        return await _client.GetAsync(endpoint);
    }

    /// <summary>
    /// Helper para fazer requisições DELETE
    /// </summary>
    protected async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        return await _client.DeleteAsync(endpoint);
    }

    /// <summary>
    /// Helper para deserializar resposta JSON
    /// </summary>
    protected async Task<T?> DeserializeResponseAsync<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, _jsonOptions);
    }
}

/// <summary>
/// Factory personalizada para testes funcionais
/// </summary>
public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove o contexto do banco de dados real
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DefaultContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Adiciona o contexto do banco de dados em memória
            services.AddDbContext<DefaultContext>(options =>
            {
                options.UseInMemoryDatabase("TestDatabase");
            });

            // Configuração específica para testes
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            // Configuração explícita do MVC para testes
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            })
            .AddApplicationPart(typeof(Program).Assembly)
            .AddControllersAsServices();

            // Adiciona serviços necessários para o MVC
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Configura serviços de teste
            ConfigureTestServices(services);
        });

        builder.Configure(app =>
        {
            // Configuração explícita de middleware para testes
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            // Popula o banco de dados de teste
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();
                SeedTestData(context);
            }
        });
    }

    private void ConfigureTestServices(IServiceCollection services)
    {
        // Aqui você pode configurar serviços específicos para teste
        // Por exemplo, mocks de serviços externos
    }

    private void SeedTestData(DefaultContext context)
    {
        // Garante que o banco foi criado
        context.Database.EnsureCreated();

        // Adiciona produtos de teste
        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Domain.Entities.Product
                {
                    Id = Guid.Parse("039e444c-028e-4d7e-b564-b30a2ca11167"),
                    Name = "Guarana",
                    Price = 7.00m,
                    Amount = 100
                },
                new Domain.Entities.Product
                {
                    Id = Guid.Parse("bbe8a82a-58ea-41d6-a6cf-073ac3adc8d9"),
                    Name = "Agua",
                    Price = 3.00m,
                    Amount = 100
                },
                new Domain.Entities.Product
                {
                    Id = Guid.Parse("27a922a0-4c35-4344-95ae-ee622ee87c80"),
                    Name = "Skol",
                    Price = 10.00m,
                    Amount = 100
                },
                new Domain.Entities.Product
                {
                    Id = Guid.Parse("f7627e62-aeaa-4224-8018-f968b47781d0"),
                    Name = "Suco",
                    Price = 5.00m,
                    Amount = 100
                }
            );
        }

        // Adiciona filiais de teste
        if (!context.Branches.Any())
        {
            context.Branches.AddRange(
                new Domain.Entities.Branch
                {
                    Id = Guid.Parse("51d8f4bb-5fb6-4f3e-b1c6-0966a9f0f34a"),
                    Name = "Matriz"
                }
            );
        }

        // Adiciona usuários de teste
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new Domain.Entities.User
                {
                    Id = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"),
                    Username = "TestUser",
                    Email = "test@example.com",
                    Password = "Test@123!",
                    Phone = "+5511999999999",
                    Status = Domain.Enums.UserStatus.Active,
                    Role = Domain.Enums.UserRole.Customer
                }
            );
        }

        context.SaveChanges();
    }
}
