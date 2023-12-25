using Projekt.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Converters
{
    public class CompanyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Count() != 2 || values[0] == null || values[1] == null) return false;
            if ((values[0] as List<Company>).Contains((Company)values[1])) return true;
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            // Logika konwersji z powrotem (jeśli potrzebna)
            return null;
        }
    }
}
