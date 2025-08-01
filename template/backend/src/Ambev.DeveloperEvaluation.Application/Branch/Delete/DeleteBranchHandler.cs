using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Branch.Delete;

public class DeleteBranchHandler : IRequestHandler<DeleteBranchCommand, DeleteBranchResponse>
{
    private readonly IBranchRepository _branchRepository;

    public DeleteBranchHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<DeleteBranchResponse> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteBranchValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _branchRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Branch with ID {request.Id} not found");

        return new DeleteBranchResponse { Success = true };
    }
} 