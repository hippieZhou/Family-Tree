using System;
using System.Windows;
using System.Windows.Data;

namespace ZQ.PrismUnityApp.Converters
{
    public class ViewQueueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int count = 0;
            if (int.TryParse(value.ToString(), out count))
            {

                return count > 1 ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
