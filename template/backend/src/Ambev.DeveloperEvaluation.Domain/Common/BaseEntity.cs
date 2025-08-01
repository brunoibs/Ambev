using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseEntity<TId> : IComparable<BaseEntity<TId>>
{
    public TId Id { get; set; } = default!;

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity<TId>? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (Id is IComparable<TId> comparable)
        {
            return comparable.CompareTo(other.Id);
        }

        return 0;
    }
}

public class BaseEntity : BaseEntity<Guid>
{
}
