using Dna;
using Chat.Core;
using WPF.Core;
using Microsoft.Extensions.DependencyInjection;
using System.ServiceModel;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;
using Windows.Media.Protection.PlayReady;
using System.Reflection.Metadata;

namespace ChatClient
{
	/// <summary>
	/// Extension methods for the <see cref="FrameworkConstruction"/>
	/// </summary>
	public static class FrameworkConstructionExtensions
	{
		/// <summary>
		/// Injects the view models needed for ChatClient application
		/// </summary>
		/// <param name="construction"></param>
		/// <returns></returns>
		public static FrameworkConstruction AddChatClientViewModels(this FrameworkConstruction construction)
		{
			// Bind to a single instance of Application view model
			construction.Services.AddSingleton<ChatApplicationViewModel>();

			// Bind to a single instance of Settings view model
			construction.Services.AddSingleton<ChatSettingsViewModel>();

			// Return the construction for chaining
			return construction;
		}

		/// <summary>
		/// Injects the client needed for ChatClient application
		/// </summary>
		/// <param name="construction"></param>
		/// <returns></returns>
		public static FrameworkConstruction AddChatClient(this FrameworkConstruction construction)
		{
			construction.Services.AddSingleton(serviceProvider =>
			{
				// Discover the server address
				var serverAddress = DiscoverServerAsync().GetAwaiter().GetResult();
				var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
				var endpointAddress = new EndpointAddress($"http://{serverAddress}:48400/api/ServiceChat");

				var channelFactory = new ChannelFactory<IServiceChat>(binding, endpointAddress);
				channelFactory.Open();
				IServiceChat serviceClient = channelFactory.CreateChannel();
				IClientChannel channel = serviceClient as IClientChannel;
				channel.Open();


				var cred = new LoginCredentialsApiModel
				{
					Username = "Username",
					Password = "pass"
				};

				var user = serviceClient.ConnectAsync(cred).Result;

				return serviceClient;
			});

			// Return the construction for chaining
			return construction;
		}

		/// <summary>
		/// Injects the ChatClient application services
		/// </summary>
		/// <param name="construction"></param>
		/// <returns></returns>
		public static FrameworkConstruction AddChatClientServices(this FrameworkConstruction construction)
		{
			// Add our task manager
			construction.Services.AddTransient<ITaskManager, BaseTaskManager>();

			// Bind a file manager
			construction.Services.AddTransient<IFileManager, BaseFileManager>();

			// Bind a UI Manager
			construction.Services.AddTransient<IUIManager, UIManager>();

			// Return the construction for chaining
			return construction;
		}

		/// <summary>
		/// Discovers the server asynchronously by listening for UDP broadcast messages.
		/// </summary>
		/// <returns>The discovered server address.</returns>
		private static async Task<string> DiscoverServerAsync()
		{
			var discoverPort = 11000;
			using (var udpClient = new UdpClient(discoverPort))
			{
				udpClient.EnableBroadcast = true;
				var endPoint = new IPEndPoint(IPAddress.Any, discoverPort);

				var result = await udpClient.ReceiveAsync();
				var serverAddress = Encoding.UTF8.GetString(result.Buffer);
				return serverAddress;
			}
		}
	}
}