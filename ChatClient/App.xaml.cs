using Dna;
using System.Windows;
using Chat.Core;
using static Dna.FrameworkDI;
using static ChatClient.DI;

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
		protected override void OnStartup(StartupEventArgs e)
		{
			// Let the base application do what it needs
			base.OnStartup(e);

			// Setup the Dna Fraimwork
			Framework.Construct<DefaultFrameworkConstruction>()
				.AddFileLogger()
				.AddChatClientViewModels()
				.AddChatClientClientServices()
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