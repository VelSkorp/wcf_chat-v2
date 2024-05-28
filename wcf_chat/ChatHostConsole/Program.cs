using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Dna;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Chat.Core;
using Chat.Relational;
using System.Net;

namespace ChatHostConsole
{
	public class Program
	{
		public static async Task Main()
		{
			// Setup the Dna Framework
			Framework.Construct<DefaultFrameworkConstruction>()
				.AddFileLogger("ChatHost.log")
				.AddHostServices()
				.AddDataStore()
				.Build();

			// Log it
			FrameworkDI.Logger.LogDebugSource("Application starting...");

			// Create Web Service Host
			var builder = WebHost.CreateDefaultBuilder()
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.ClearProviders();
					logging.AddProvider(ServiceProviderServiceExtensions.GetService<ILoggerProvider>(Framework.Provider));
				})
				.ConfigureKestrel(options =>
				{
					var port = FrameworkDI.Configuration.GetSection("ServerPort").Value;
					options.Listen(IPAddress.Any, int.Parse(port));
				})
				.UseStartup<BasicHttpBindingStartup>();

			var mServiceHost = builder.Build();

			var cancellationTokenSource = new CancellationTokenSource();
			var announcementServiceTask = UdpAnnouncementService.StartAsync(cancellationTokenSource.Token);
			
			await mServiceHost.RunAsync();
		}
	}
}