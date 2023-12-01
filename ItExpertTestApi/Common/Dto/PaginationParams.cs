using System.ComponentModel.DataAnnotations;

namespace ItExpertTestApi
{
    public record class PaginationParams(int? Page, int? PageSize) : IValidatableObject
    {
        public virtual IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            List<ValidationResult> results = new();
            if (Page < 0)
            {
                results.Add(new ValidationResult($"{nameof(Page)} < 0"));
            }
            if (PageSize < 0)
            {
                results.Add(new ValidationResult($"{nameof(PageSize)} < 0"));
            }
            return results;
        }
    }
}
