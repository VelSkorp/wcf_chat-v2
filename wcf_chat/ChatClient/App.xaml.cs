using Dna;
using System.Windows;
using Chat.Core;
using static Dna.FrameworkDI;
using static ChatClient.DI;
using CoreWCF;

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
				.AddChatClientServices()
				.AddChatClient()
				.Build();

			// Find IServiceChat endpoint
			EndpointAddress endpointAddress = FindServiceChatAddress();

			// Connect to the discovered service endpoint  
			//Client.Endpoint.Address = endpointAddress;

			// Log it
			Logger.LogDebugSource("Application starting...");

			ViewModelApplication.GoToPage(ApplicationPage.Chat);

			// Show the main window
			Current.MainWindow = new MainWindow();
			Current.MainWindow.Show();
		}

		private EndpointAddress FindServiceChatAddress()
		{
			//// Create DiscoveryClient  
			//var discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());

			//// Find IServiceChat endpoints
			//FindResponse findResponse = discoveryClient.Find(new FindCriteria(typeof(Chat.Core.Proxy.IServiceChat)));

			//if (findResponse.Endpoints.Count > 0)
			//{
			//	return findResponse.Endpoints[0].Address;
			//}
			//else
			//{
			//	return null;
			//}
			return null;
		}
	}
}