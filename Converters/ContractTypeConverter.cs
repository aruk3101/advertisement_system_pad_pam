using Projekt.Models.Common.Enumerated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Converters
{
    public class ContractTypeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Count() != 2 || values[0] == null || values[1] == null) return false;
            if ((values[0] as List<ContractType>).Contains((ContractType)values[1])) return true;
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            // Logika konwersji z powrotem (jeśli potrzebna)
            return null;
        }
    }
}
