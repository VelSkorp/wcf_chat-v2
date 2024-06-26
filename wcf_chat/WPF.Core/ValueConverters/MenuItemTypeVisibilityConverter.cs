﻿using Chat.Core;
using System;
using System.Globalization;
using System.Windows;

namespace WPF.Core
{
	/// <summary>
	/// A converter that takes in a <see cref="MenuItemType"/> and returns a <see cref="Visibility"/>
	/// based on the given Parameter being the same as the menu item type
	/// </summary>
	public class MenuItemTypeVisibilityConverter : BaseValueConverter<MenuItemTypeVisibilityConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// If we have no paramenter return invisible
			if (parameter is null)
			{
				return Visibility.Collapsed;
			}

			// Try and convert parameter string to enum
			if (!Enum.TryParse(parameter as string, out MenuItemType type))
			{
				return Visibility.Collapsed;
			}

			// Return visible if the parameter mathes the type
			return (MenuItemType)value == type ? Visibility.Visible : Visibility.Collapsed;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}