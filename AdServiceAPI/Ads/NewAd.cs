using System.ComponentModel.DataAnnotations;

namespace AdServiceAPI.Ads
{
    public class NewAd : IValidatableObject
    {
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Mellan 3 och 50")]
        [Required]
        public string Title { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required]
        public string Product { get; set; } = string.Empty;

        [Range(0, 100000, ErrorMessage =
        "Skriv ett tal mellan 0 och 100000")]
        public decimal Price { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate == default || EndDate == default)
            {
                yield return new ValidationResult(
                    "Startdatum och slutdatum måste anges.",
                    [nameof(StartDate), nameof(EndDate)]);

                yield break;
            }

            if (EndDate < StartDate)
            {
                yield return new ValidationResult(
                    "Slutdatum kan inte vara före startdatum.",
                    [nameof(EndDate)]);
            }
        }
    }
}

