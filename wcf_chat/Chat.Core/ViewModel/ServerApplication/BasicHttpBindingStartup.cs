using System;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using CoreWCF.Channels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Dna;
using System.Net;

namespace Chat.Core
{
	public class BasicHttpBindingStartup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			//Enable CoreWCF Services, with metadata(WSDL) support
			services.AddServiceModelServices()
					.AddServiceModelMetadata()
					.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>()
					.AddTransient<ServiceChat>();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseServiceModel(builder =>
			{
				builder.AddService<ServiceChat>(serviceOptions =>
				{
					serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
				})
				.AddServiceEndpoint<ServiceChat, IServiceChat>(new BasicHttpBinding(), "/api/ServiceChat");

				// Configure WSDL to be available
				var serviceMetadataBehavior = app.ApplicationServices.GetRequiredService<ServiceMetadataBehavior>();
				var port = FrameworkDI.Configuration.GetSection("ServerPort").Value;
				serviceMetadataBehavior.HttpGetEnabled = true;
				serviceMetadataBehavior.HttpGetUrl = new Uri($"http://{IPAddress.Any}:{port}/api/Metadata");
			});
		}
	}
}