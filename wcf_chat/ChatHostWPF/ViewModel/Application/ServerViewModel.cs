using System;
using System.Windows.Input;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Dna;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

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
				var builder = WebHost.CreateDefaultBuilder()
					.ConfigureLogging((hostingContext, logging) =>
					{
						logging.ClearProviders();
						logging.AddProvider(ServiceProviderServiceExtensions.GetService<ILoggerProvider>(Framework.Provider));
					})
					.UseStartup<BasicHttpBindingStartup>();

				mServiceHost = builder.Build();
				mServiceHost.RunAsync();

				IsStarted = true;

				Log.Add($"[Data] Host stated\n");
				FrameworkDI.Logger.LogInformationSource("Host stated");

				// TODO: add health checker
				Log.Add($"[Data] To check the server, go to the URL: {FrameworkDI.Configuration.GetSection("Kestrel:Endpoints:Https:Url").Value}/api/Service\n");
				Log.Add($"[Data] To get metadata, go to the URL: {FrameworkDI.Configuration.GetSection("Kestrel:Endpoints:Https:Url").Value}/api/Metadata\n");
			}
			catch (Exception ex)
			{
				Log.Add($"[Error] {ex.Message}\n");
				FrameworkDI.Logger.LogDebugSource(ex.Message);
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
				FrameworkDI.Logger.LogInformationSource("Host stopped");
			}
			catch (Exception ex)
			{
				Log.Add($"[Error] {ex.Message}\n");
				FrameworkDI.Logger.LogDebugSource(ex.Message);
			}
			finally
			{
				Log = new List<string>(Log);
			}
		}

		#endregion
	}
}