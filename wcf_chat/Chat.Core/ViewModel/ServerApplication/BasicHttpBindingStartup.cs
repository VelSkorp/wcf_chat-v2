using System;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Dna;

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
				}).AddServiceEndpoint<ServiceChat, IServiceChat>(new BasicHttpBinding(), "/api/Service");

				// Configure WSDL to be available
				var serviceMetadataBehavior = app.ApplicationServices.GetRequiredService<ServiceMetadataBehavior>();
				serviceMetadataBehavior.HttpGetEnabled = true;
				serviceMetadataBehavior.HttpGetUrl = new Uri($"{FrameworkDI.Configuration.GetSection("Kestrel:Endpoints:Https:Url").Value}/api/Metadata");
			});
		}
	}
}