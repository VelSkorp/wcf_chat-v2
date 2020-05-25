using System;
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
					// Set it's margin to the given value

					// Ignore any non-text entry controls
					if (!(child is TextEntryControl control) && !(child is PasswordEntryControl))
						continue;

					// Get the label from the text entry or password entry
					var label = child is TextEntryControl ? (child as TextEntryControl).Label : (child as PasswordEntryControl).Label;

					label.SizeChanged += (ss, eee) =>
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
				if (!(child is TextEntryControl) && !(child is PasswordEntryControl))
					continue;

				// Get the label from the text entry or password entry
				var label = child is TextEntryControl ? (child as TextEntryControl).Label : (child as PasswordEntryControl).Label;

				// Find if this value is larger than the other controls
				maxSize = Math.Max(maxSize, label.RenderSize.Width + label.Margin.Left + label.Margin.Right);
			}

			// Create a grid length converter
			var gridLength = (GridLength)new GridLengthConverter().ConvertFromString(maxSize.ToString());

			// For each child...
			foreach (var child in panel.Children)
			{
				if (child is TextEntryControl text)
					// Set each controls  LabelWidth value to the max size
					text.LabelWidth = gridLength;
				else if (child is PasswordEntryControl pass)
					// Set each controls LabelWidth value to the max size
					pass.LabelWidth = gridLength;
			}
		}
	}
}