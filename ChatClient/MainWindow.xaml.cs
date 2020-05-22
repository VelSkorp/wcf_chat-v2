using System.Windows;

namespace ChatClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			DataContext = new WindowViewModel(this);
		}

		private void AppWindow_Deactivated(object sender, System.EventArgs e)
		{
			// TODO: вынести из кода позади
			// Show overlay if we lose focus
			(DataContext as WindowViewModel).DimmableOverlayVisible = true;
		}

		private void AppWindow_Activated(object sender, System.EventArgs e)
		{
			// Hide overlay if we are focused
			(DataContext as WindowViewModel).DimmableOverlayVisible = false;
		}
	}
}