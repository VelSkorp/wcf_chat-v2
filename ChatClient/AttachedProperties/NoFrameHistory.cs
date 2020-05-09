using System.Windows;
using System.Windows.Controls;

namespace ChatClient
{
	/// <summary>
	/// The NoFrameHistory attched property for crating <see cref="Frame"/> thet never shows navigation
	/// and keeps the navigation history empty
	/// </summary>
	public class NoFrameHistory : BaseAttachedProperty<NoFrameHistory, bool>
	{
		public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			// Get the frame
			var frame = (sender as Frame);

			// Hide navigation bar
			frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;

			// Clear history on navigate
			frame.Navigated += (ss, ee) => ((Frame)ss).NavigationService.RemoveBackEntry();
		}
	}
}