using Phuoc_C3_B1.Models;
using System;
using System.Globalization;
using System.Windows.Data;


namespace Phuoc_C3_B1.Converters
{
    public class ConvertToBooleanIfProductIsFood : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a food");

            return (value is Food) ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
