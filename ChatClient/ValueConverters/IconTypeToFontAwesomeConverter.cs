﻿using ChatClient.Core;
using System;
using System.Globalization;

namespace ChatClient
{
	/// <summary>
	/// A converter that takes in a <see cref="IconType"/> and returns
	/// the FontAwesome string for that icon
	/// </summary>
	public class IconTypeToFontAwesomeConverter : BaseValueConverter<IconTypeToFontAwesomeConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ((IconType)value).ToFontAwesome();

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}
}