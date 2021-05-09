using Dna;
using System.Windows;
using ChatHost.Core;
using static Dna.FrameworkDI;
using static ChatHostWPF.DI;

namespace ChatHostWPF
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
		protected override void OnStartup(StartupEventArgs e)
		{
			// Let the base application do what it needs
			base.OnStartup(e);

			// Setup the Dna Fraimwork
			Framework.Construct<DefaultFrameworkConstruction>()
				.AddFileLogger()
				.AddChatHostViewModels()
				.AddChatClientHostServices()
				.Build();

			// Log it
			Logger.LogDebugSource("Application starting...");

			ViewModelApplication.GoToPage(ApplicationPage.Login);

			// Show the main window
			Current.MainWindow = new MainWindow();
			Current.MainWindow.Show();
		}
	}
}