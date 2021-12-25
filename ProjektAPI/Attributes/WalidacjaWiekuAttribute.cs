using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Property)]
    public class WalidacjaWiekuAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool sprawdzenieDaty = DateTime.TryParse(value.ToString(), out DateTime parsowanieDaty);
            if (!sprawdzenieDaty)
                return new ValidationResult("Niepoprawny format daty");
            else
            {
                var dzis = DateTime.Now;
                try
                {
                    if (parsowanieDaty > dzis) // error
                        return new ValidationResult("Nie możesz podać daty w przyszłości");
                    else
                        return ValidationResult.Success;
                }
                catch (Exception e)
                {
                    return new ValidationResult(e.Message);
                }
            }
        }
    }
}
