using System;
using System.Globalization;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Data;

namespace StackLeader.Converters
{
    public class FileButtonView: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Проверяем, есть ли файл для скачивания
            if (value != null && value is byte[] fileData && fileData.Length > 0)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
