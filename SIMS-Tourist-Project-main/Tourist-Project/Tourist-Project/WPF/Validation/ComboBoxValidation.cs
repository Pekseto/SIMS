using System.Globalization;
using System.Windows.Controls;

namespace Tourist_Project.WPF.Validation
{

    public class ComboBoxValidation : ValidationRule
    {
        public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
        {
            return value is ComboBoxItem ? new ValidationResult(false, "You have to select item.") : new ValidationResult(true, null);
        }
    }

}