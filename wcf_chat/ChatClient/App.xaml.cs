using Dna;
using System.Windows;

namespace ChatClient
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// Custom startup so we load our IoC immediately before anything else
		/// </summary>
		/// <param name="e"></param>
		protected override async void OnStartup(StartupEventArgs e)
		{
			// Let the base application do what it needs
			base.OnStartup(e);

			// Setup the Dna Framework
			Framework.Construct<DefaultFrameworkConstruction>()
				.AddFileLogger()
				.AddChatClientViewModels()
				.AddChatClientServices()
				.AddChatClient()
				.Build();

			// Log it
			FrameworkDI.Logger.LogDebugSource("Application starting...");

			// Show the main window
			Current.MainWindow = new MainWindow();
			Current.MainWindow.Show();
		}
	}
}