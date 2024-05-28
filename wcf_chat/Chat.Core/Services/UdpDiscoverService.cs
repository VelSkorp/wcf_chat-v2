using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dna;

namespace Chat.Core
{
	/// <summary>
	/// Provides a service for discover the server address via UDP.
	/// </summary>
	public static class UdpDiscoverService
	{
		// The port used for UDP broadcasting.
		private static readonly int Port = 11000;

		/// <summary>
		/// Gets the server address asynchronously.
		/// </summary>
		/// <returns>A task representing the asynchronous operation. The task result contains the server address.</returns>
		/// <remarks>
		/// This method retrieves the server address from the configuration. 
		/// If an explicit server address is set in the configuration, it uses that address. 
		/// Otherwise, it discovers the server address dynamically and combines it with the server port.
		/// </remarks>
		public static async Task<string> GetServerAddressAsync()
		{
			var explicitAddressValue = FrameworkDI.Configuration.GetSection("ExplicitServerAddress").Value;
			var isExplicitAddressNotSet = string.IsNullOrEmpty(explicitAddressValue);
			return isExplicitAddressNotSet ? $"http://{await DiscoverServerAsync()}" : explicitAddressValue;
		}

		/// <summary>
		/// Discovers the server asynchronously by listening for UDP broadcast messages.
		/// </summary>
		/// <returns>The discovered server address.</returns>
		public static async Task<string> DiscoverServerAsync()
		{
			using (var udpClient = new UdpClient(Port))
			{
				udpClient.EnableBroadcast = true;
				var endPoint = new IPEndPoint(IPAddress.Any, Port);

				var result = await udpClient.ReceiveAsync();
				var serverAddress = Encoding.UTF8.GetString(result.Buffer);
				return serverAddress;
			}
		}
	}
}