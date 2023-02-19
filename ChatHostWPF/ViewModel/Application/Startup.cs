using Chat.Core;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ChatHostWPF
{
	public class BasicHttpBindingStartup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			//Enable CoreWCF Services, with metadata (WSDL) support
			services.AddServiceModelServices()
					.AddServiceModelMetadata();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseServiceModel(builder =>
			{
				builder.AddService<ServiceChat>(serviceOptions => { })
					.AddServiceEndpoint<ServiceChat, IServiceChat>(new BasicHttpBinding(), "/ChatService/basicHttp");

				// Configure WSDL to be available
				var serviceMetadataBehavior = app.ApplicationServices.GetRequiredService<ServiceMetadataBehavior>();
				serviceMetadataBehavior.HttpGetEnabled = true;
			});
		}
	}
}