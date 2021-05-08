﻿using ChatClient;
using System;
using System.Globalization;

namespace ChatClient
{
	/// <summary>
	/// A converter that takes in a <see cref="BaseViewModel"/> and returns a specific UI control
	/// that should bind to that type of ViewModel
	/// </summary>
	public class PopupContentConverter : BaseValueConverter<PopupContentConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is ChatAttachmentPopupMenuViewModel basePopup)
				return new VerticalMenu { DataContext = basePopup.Content };

			return null;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}
}