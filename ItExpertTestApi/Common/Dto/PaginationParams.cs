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
                results.Add(new ValidationResult(
                    "Must be >= 1",
                    new[] { nameof(Page) }));
            }
            if (PageSize < 1)
            {
                results.Add(new ValidationResult(
                    "Must be >= 1",
                    new[] { nameof(PageSize) }));
            }
            return results;
        }
    }
}
