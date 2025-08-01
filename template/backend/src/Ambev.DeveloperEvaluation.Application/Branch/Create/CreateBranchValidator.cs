using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branch.Create;

public class CreateBranchValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchValidator()
    {
        RuleFor(b => b.Name).NotEmpty().Length(2, 100);
    }
} 