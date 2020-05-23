﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace ChatClient
{
	/// <summary>
	/// Match the label width of all text entry controls inside this panel 
	/// </summary>
	public class TextEntryWidthMatcherProperty : BaseAttachedProperty<TextEntryWidthMatcherProperty, bool>
	{
		public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			// Get the panel (grid typically)
			var panel = (sender as Panel);

			// Call SetWidths initialy (this also helps design time to work)
			SetWidth(panel);

			// Wait for panel to load
			RoutedEventHandler onLoaded = null;
			onLoaded = (s, ee) =>
			{
				// Unhhok
				panel.Loaded -= onLoaded;

				// Set width
				SetWidth(panel);

				// Loop each child 
				foreach (var child in panel.Children)
				{
					// Ignore any non-text entry controls
					if (!(child is TextEntryControl control))
						continue;

					// Sets it's margin to the given value
					control.Label.SizeChanged += (ss, eee) =>
					{
						// Update widths
						SetWidth(panel);
					};
				}
			};

			// Hook into the Loaded event 
			panel.Loaded += onLoaded;
		}

		/// <summary>
		/// Update all child text entry controls so their widths match the larges width of the group
		/// </summary>
		/// <param name="panel">The panel containing the text entry controls</param>
		private void SetWidth(Panel panel)
		{
			// Keep	track of the maximum width
			var maxSize = 0d;

			// For each child...
			foreach (var child in panel.Children)
			{
				// Ignore any non-text entry controls
				if (!(child is TextEntryControl control))
					continue;

				// Find if this value is larger than the other controls
				maxSize = Math.Max(maxSize, control.Label.RenderSize.Width + control.Label.Margin.Left + control.Label.Margin.Right);
			}

			// Create a grid length converter
			var gridLength = (GridLength)new GridLengthConverter().ConvertFromString(maxSize.ToString());
			

			// For each child...
			foreach (var child in panel.Children)
			{
				// Ignore any non-text entry controls
				if (!(child is TextEntryControl control))
					continue;

				// Set each controls LabelWidth value to the max size
				control.LabelWidth = gridLength;
			}

		}
	}
}