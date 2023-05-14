using System.Globalization;
using System.Windows.Controls;

namespace Tourist_Project.WPF.Validation
{

    public class EmptyStringValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                return string.IsNullOrWhiteSpace((string)value) ? 
                    new ValidationResult(false, "Field mustn't be empty!") 
                    : ValidationResult.ValidResult;
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occurred.");
            }
        }
    }

}