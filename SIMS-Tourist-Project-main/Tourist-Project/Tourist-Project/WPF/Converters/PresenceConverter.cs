using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.Converters
{
    public class PresenceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert the Presence value to Nullable<bool>
            if (value is Presence presence)
            {
                // Define your conversion logic here
                // For example, if presence == Presence.Present, return true; otherwise, return false.
                return presence == Presence.Yes;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
