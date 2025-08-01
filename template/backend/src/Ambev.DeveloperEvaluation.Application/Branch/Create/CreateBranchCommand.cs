using MediatR;
using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Application.Branch.Create;

/// <summary>
/// Command para criar uma nova filial.
/// </summary>
public class CreateBranchCommand : IRequest<CreateBranchResult>
{
    public string Name { get; set; } = string.Empty;

    public ValidationResultDetail Validate()
    {
        var validator = new CreateBranchValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
} 