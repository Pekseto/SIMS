using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tourist_Project.WPF.Validation
{
    public class StayingDaysValidation : ValidationRule
    {
         public StayingDaysValidation()
            {

            }
        public String Min { get; set; }    

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int stayingDays = 0;
            int minTemp = Convert.ToInt32(Min);
            
            try
            {
                if (((string)value).Length > 0)
                    stayingDays = Int32.Parse((String)value);
                return new ValidationResult(true, null);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Unknown error occured");
                throw;
            }
            if(stayingDays < minTemp)
            {
                return new ValidationResult(false, "Number of staying days must be larger than: {Min}");
            }
        }




    }
}
