using System;
using System.Globalization;
using System.Windows.Controls;

namespace Tourist_Project.WPF.Validation
{

    public class EmptyStingValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                return ((string)value).Length > 0
                    ? new ValidationResult(true, null)
                    : new ValidationResult(false, "Field must not be empty");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occurred.");
            }
        }
    }

}