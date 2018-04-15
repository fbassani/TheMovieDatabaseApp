using System;
using System.Globalization;
using Xamarin.Forms;

namespace TheMovieDatabaseApp.Converters
{
    public class PathToImageUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            return path == null ? null : $"{Settings.ImagesBaseUrl}{path}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}