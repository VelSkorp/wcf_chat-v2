using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace Chat.Core.ServiceWCF
{
	public static class UdpAnnouncementService
	{
		private static readonly int Port = 11000;

		public static async Task StartAsync(CancellationToken cancellationToken)
		{
			using (var udpClient = new UdpClient())
			{
				udpClient.EnableBroadcast = true;

				var serverAddress = await GetServerAddressAsync();
				var announcementMessage = Encoding.UTF8.GetBytes(serverAddress);
				var endPoint = new IPEndPoint(IPAddress.Broadcast, Port);

				while (!cancellationToken.IsCancellationRequested)
				{
					udpClient.Send(announcementMessage, announcementMessage.Length, endPoint);
					await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
				}
			}
		}

		private static async Task<string> GetServerAddressAsync()
		{
			if (IsRunningInDocker())
			{
				return await GetDockerContainerAddressAsync();
			}
			return GetLocalMachineAddress();
		}

		private static bool IsRunningInDocker()
		{
			// Check if running in Docker by checking if "/.dockerenv" file exists
			return File.Exists("/.dockerenv");
		}

		private static string GetLocalMachineAddress()
		{
			// Get the local machine address
			var serverAddresses = Dns.GetHostAddresses(Dns.GetHostName()).Where(address => address.AddressFamily == AddressFamily.InterNetwork);
			return serverAddresses.First().ToString();
		}

		private static async Task<string> GetDockerContainerAddressAsync()
		{
			// Get the docker container address
			var hostEntry = await Dns.GetHostEntryAsync("host.docker.internal");
			return hostEntry.AddressList.First(address => address.AddressFamily == AddressFamily.InterNetwork).ToString();
		}
	}
}