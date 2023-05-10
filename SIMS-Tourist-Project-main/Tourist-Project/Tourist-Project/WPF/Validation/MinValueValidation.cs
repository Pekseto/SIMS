using System;
using System.Globalization;
using System.Windows.Controls;

namespace Tourist_Project.WPF.Validation
{
    public class MinValueValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!int.TryParse(value?.ToString(), out int num))
                return new ValidationResult(false, "Value has to be int.");
            if(num > 0) return ValidationResult.ValidResult;

            return new ValidationResult(false, "Value has to be 1 or more.");

        }
    } 
}
