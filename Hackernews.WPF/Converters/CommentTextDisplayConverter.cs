using System;
using System.Windows.Data;

namespace Hackernews.WPF.Converters
{
	public class CommentTextDisplayConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value != null)
			{
				string s = value.ToString();

				int endIndex = s.Length > 50 ? 50 : s.Length;

				return s.Substring(0, endIndex) + "...";
			}
			throw new ArgumentException($"'value' must be of type string.");
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}