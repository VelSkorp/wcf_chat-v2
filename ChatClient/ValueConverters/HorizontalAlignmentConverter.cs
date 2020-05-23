﻿using System;
using System.Globalization;
using System.Windows;

namespace ChatClient
{
	/// <summary>
	/// A converter that takes in the core horizontal alignment enum and coverts it to a WPF alignment
	/// </summary>
	public class HorizontalAlignmentConverter : BaseValueConverter<HorizontalAlignmentConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (HorizontalAlignment)value;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}
}