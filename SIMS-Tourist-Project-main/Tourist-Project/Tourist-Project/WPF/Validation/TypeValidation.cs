using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tourist_Project.WPF.Validation
{
    public class TypeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {   
            try
            {
                var num = value as string;
                int n;
                if(int.TryParse(num, out n))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Please enter a positive whole number");//ubaciti jos jednu validaciju za min vrednosti kod StayngDays i GuestNum

            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured");
            }
        }

    }
}
