using System;
using System.Net;
using System.Windows.Input;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ChatHostWPF
{
	/// <summary>
	/// The View Model for a login screen
	/// </summary>
	public class ServerViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The flag that indicates server is started or not
		/// </summary>
		public bool IsStarted { get; private set; } = false;

		/// <summary>
		/// The server message log
		/// </summary>
		public List<string> Log { get; private set; } = new List<string>();

		#endregion

		#region Private Members

		/// <summary>
		/// ServiceHost for hosting ServiceChat service
		/// </summary>
		private IWebHost mServiceHost;

		private const int HTTP_PORT = 48400;

		#endregion

		#region Commands

		/// <summary>
		/// The command to start the server
		/// </summary>
		public ICommand StartTheServerCommand { get; set; }

		/// <summary>
		/// The command to disable the server
		/// </summary>
		public ICommand DisableTheServerCommand { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ServerViewModel()
		{
			// Create commands
			StartTheServerCommand = new RelayCommand(StartTheServer);
			DisableTheServerCommand = new RelayCommand(DisableTheServer);
		}

		#endregion

		#region Commands Methods

		/// <summary>
		/// Start the server
		/// </summary>
		public void StartTheServer()
		{
			try
			{
				// Create base address for ServiceHost using DNS host name of the local computer
				//var baseAdress = new Uri($"net.tcp://{Dns.GetHostName()}");

				var builder = WebHost.CreateDefaultBuilder()
					.UseKestrel(options =>
					{
						options.Listen(IPAddress.Any, HTTP_PORT);
					})
					.UseStartup<BasicHttpBindingStartup>();

				mServiceHost = builder.Build();
				mServiceHost.RunAsync();

				IsStarted = true;

				Log.Add($"[Data] Host stated\n");
			}
			catch (Exception ex)
			{
				Log.Add($"[Error] {ex.Message}\n");
			}
			finally
			{
				Log = new List<string>(Log);
			}
		}

		/// <summary>
		/// Start the server
		/// </summary>
		public void DisableTheServer()
		{
			try
			{
				// Close the ServiceHost
				mServiceHost.StopAsync();

				IsStarted = false;

				Log.Add($"[Data] Host stopped\n");
			}
			catch (Exception ex)
			{
				Log.Add($"[Error] {ex.Message}\n");
			}
			finally
			{
				Log = new List<string>(Log);
			}
		}

		#endregion
	}
}