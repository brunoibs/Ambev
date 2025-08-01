using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Branch.List;

public class ListBranchResult
{
    public List<BranchListItem> Branches { get; set; } = new();
}

public class BranchListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
} 