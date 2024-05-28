using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Dna;

namespace Chat.Core
{
	/// <summary>
	/// Provides a service for broadcasting the server address via UDP.
	/// </summary>
	public static class UdpAnnouncementService
	{
		// The port used for UDP broadcasting.
		private static readonly int Port = 11000;

		/// <summary>
		/// Starts the UDP announcement service asynchronously.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <remarks>
		/// This method continuously broadcasts the server address every 5 seconds until cancellation is requested.
		/// It uses UDP broadcasting to announce the server address to the network.
		/// </remarks>
		/// <exception cref="SocketException">Thrown if there is an error accessing the socket.</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the UdpClient has been disposed.</exception>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		public static async Task StartAsync(CancellationToken cancellationToken)
		{
			using (var udpClient = new UdpClient())
			{
				udpClient.EnableBroadcast = true;

				var port = FrameworkDI.Configuration.GetSection("ServerPort").Value;
				var serverAddress = $"{await GetServerAddressAsync()}:{port}";
				var announcementMessage = Encoding.UTF8.GetBytes(serverAddress);
				var endPoint = new IPEndPoint(IPAddress.Broadcast, Port);

				while (!cancellationToken.IsCancellationRequested)
				{
					udpClient.Send(announcementMessage, announcementMessage.Length, endPoint);
					await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
				}
			}
		}

		/// <summary>
		/// Gets the server address asynchronously.
		/// </summary>
		/// <returns>A task representing the asynchronous operation. The task result contains the server address.</returns>
		/// <remarks>
		/// This method determines whether the application is running in a Docker container.
		/// If it is running in Docker, it retrieves the Docker container's address.
		/// Otherwise, it retrieves the local machine's address.
		/// </remarks>
		private static async Task<string> GetServerAddressAsync()
		{
			if (IsRunningInDocker())
			{
				return await GetDockerContainerAddressAsync();
			}
			return GetLocalMachineAddress();
		}

		/// <summary>
		/// Determines whether the application is running in a Docker container.
		/// </summary>
		/// <returns>True if running in Docker; otherwise, false.</returns>
		/// <remarks>
		/// This method checks for the existence of the "/.dockerenv" file, which indicates a Docker environment.
		/// </remarks>
		private static bool IsRunningInDocker()
		{
			// Check if running in Docker by checking if "/.dockerenv" file exists
			return File.Exists("/.dockerenv");
		}

		/// <summary>
		/// Gets the local machine's IP address.
		/// </summary>
		/// <returns>The local machine's IP address.</returns>
		/// <remarks>
		/// This method retrieves the local machine's IP address by querying the DNS for host addresses.
		/// It filters out loopback addresses and selects the first available IPv4 address.
		/// If no suitable address is found, it defaults to "127.0.0.1".
		/// </remarks>
		private static string GetLocalMachineAddress()
		{
			// Get the local machine address
			var serverAddresses = Dns.GetHostAddresses(Dns.GetHostName())
									  .Where(address => address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(address));
			return serverAddresses.FirstOrDefault()?.ToString() ?? "127.0.0.1";
		}

		/// <summary>
		/// Gets the Docker container's IP address asynchronously.
		/// </summary>
		/// <returns>A task representing the asynchronous operation. The task result contains the Docker container's IP address.</returns>
		/// <remarks>
		/// This method retrieves the Docker container's IP address by resolving the "host.docker.internal" hostname.
		/// It selects the first available IPv4 address from the resolved addresses.
		/// </remarks>
		/// <exception cref="SocketException">Thrown if there is an error accessing the socket.</exception>
		/// <exception cref="ArgumentException">Thrown if the hostname is invalid.</exception>
		private static async Task<string> GetDockerContainerAddressAsync()
		{
			// Get the docker container address
			var hostEntry = await Dns.GetHostEntryAsync("host.docker.internal");
			return hostEntry.AddressList.First(address => address.AddressFamily == AddressFamily.InterNetwork).ToString();
		}
	}
}