using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Common.Validation;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? Enumerable.Empty<IValidator<TRequest>>();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        if (next == null)
            throw new ArgumentNullException(nameof(next));

        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                var validationMessage = FormatValidationMessage(failures, typeof(TRequest).Name);
                throw new ValidationException(validationMessage, failures);
            }
        }

        return await next();
    }

    private static string FormatValidationMessage(IEnumerable<ValidationFailure> failures, string requestTypeName)
    {
        if (!failures.Any())
            return "Validation passed successfully.";

        var errorMessages = failures
            .Select(f => 
            {
                var errorDetail = $"- {f.PropertyName}: {f.ErrorMessage}";
                
                if (!string.IsNullOrEmpty(f.ErrorCode))
                    errorDetail += $" (Código: {f.ErrorCode})";
                
                if (f.AttemptedValue != null)
                    errorDetail += $" (Valor: {f.AttemptedValue})";
                
                return errorDetail;
            })
            .ToList();

        var summary = $"Falha na validação do {requestTypeName}. Encontrados {failures.Count()} erro(s):";
        
        var details = string.Join("\n", errorMessages);
        
        return $"{summary}\n{details}";
    }
}