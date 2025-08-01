using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branch.Get;

public class GetBranchValidator : AbstractValidator<GetBranchCommand>
{
    public GetBranchValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Branch ID is required");
    }
} 