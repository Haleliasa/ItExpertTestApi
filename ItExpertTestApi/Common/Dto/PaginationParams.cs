using System.ComponentModel.DataAnnotations;

namespace ItExpertTestApi
{
    public record class PaginationParams(int? Page, int? PageSize) : IValidatableObject
    {
        public virtual IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            List<ValidationResult> results = new();
            if (Page < 1)
            {
                results.Add(new ValidationResult($"{nameof(Page)} < 1"));
            }
            if (PageSize < 1)
            {
                results.Add(new ValidationResult($"{nameof(PageSize)} < 1"));
            }
            return results;
        }
    }
}
