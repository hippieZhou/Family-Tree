using System;
using System.Windows;
using System.Windows.Data;

namespace ZQ.PrismUnityApp.Converters
{
    public class ViewQueueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int.Parse(value.ToString()) > 0) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
