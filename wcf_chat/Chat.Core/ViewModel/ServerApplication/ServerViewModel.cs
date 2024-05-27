using System;
using System.Windows.Input;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Dna;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using System.Threading.Tasks;
using System.Threading;
using Chat.Core.ServiceWCF;

namespace Chat.Core
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
		public bool IsStarted { get; set; } = false;

		/// <summary>
		/// The server message log
		/// </summary>
		public List<string> Log { get; set; } = new List<string>();

		/// <summary>
		/// A flag indicating if the start/stop server command is running
		/// </summary>
		public bool StartStopIsRunning { get; set; }

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

		#region Private Members

		/// <summary>
		/// ServiceHost for hosting ServiceChat service
		/// </summary>
		private IWebHost mServiceHost;

		/// <summary>
		/// ServiceHost for hosting ServiceChat service
		/// </summary>
		private CancellationTokenSource mCancellationTokenSource;

		/// <summary>
		/// ServiceHost for hosting ServiceChat service
		/// </summary>
		private Task mAnnouncementServiceTask;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ServerViewModel()
		{
			// Create commands
			StartTheServerCommand = new RelayAsyncCommand(StartTheServerAsync);
			DisableTheServerCommand = new RelayAsyncCommand(DisableTheServerAsync);

			// Create Web Service Host
			var builder = WebHost.CreateDefaultBuilder()
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.ClearProviders();
					logging.AddProvider(ServiceProviderServiceExtensions.GetService<ILoggerProvider>(Framework.Provider));
				})
				.UseStartup<BasicHttpBindingStartup>();

			mServiceHost = builder.Build();
		}

		#endregion

		#region Commands Methods

		/// <summary>
		/// Start the server
		/// </summary>
		public async Task StartTheServerAsync()
		{
			await RunCommandAsync(() => StartStopIsRunning, async () =>
			{
				try
				{
					mServiceHost.RunAsync();

					IsStarted = true;

					mCancellationTokenSource = new CancellationTokenSource();
					mAnnouncementServiceTask = UdpAnnouncementService.StartAsync(mCancellationTokenSource.Token);

					Log.Add($"[Data] Host stated\n");
					FrameworkDI.Logger.LogInformationSource("Host stated");

					// TODO: add health checker
					Log.Add($"[Data] To check the server, go to the URL: {FrameworkDI.Configuration.GetSection("Kestrel:Endpoints:Http:Url").Value}/api/ServiceChat\n");
					Log.Add($"[Data] To get metadata, go to the URL: {FrameworkDI.Configuration.GetSection("Kestrel:Endpoints:Http:Url").Value}/api/Metadata\n");

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
			});
		}

		/// <summary>
		/// Start the server
		/// </summary>
		public async Task DisableTheServerAsync()
		{
			await RunCommandAsync(() => StartStopIsRunning, async () =>
			{
				try
				{
					await mServiceHost.StopAsync();

					mCancellationTokenSource.Cancel();

					await mAnnouncementServiceTask;

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
			});
		}

		#endregion
	}
}