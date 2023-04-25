using Dna;
using Chat.Core;
using Microsoft.Extensions.DependencyInjection;
//using Chat.Core.Proxy;

namespace ChatClient
{
	/// <summary>
	/// Extension methods for the <see cref="FrameworkConstruction"/>
	/// </summary>
	public static class FrameworkConstructionExtensions
	{
		/// <summary>
		/// Injects the view models needed for ChatClient application
		/// </summary>
		/// <param name="construction"></param>
		/// <returns></returns>
		public static FrameworkConstruction AddChatClientViewModels(this FrameworkConstruction construction)
		{
			// Bind to a single instance of Application view model
			construction.Services.AddSingleton<ApplicationViewModel>();

			// Bind to a single instance of Settings view model
			construction.Services.AddSingleton<SettingsViewModel>();

			// Return the construction for chaining
			return construction;
		}

		/// <summary>
		/// Injects the client needed for ChatClient application
		/// </summary>
		/// <param name="construction"></param>
		/// <returns></returns>
		public static FrameworkConstruction AddChatClient(this FrameworkConstruction construction)
		{
			// Bind to a single instance of ServiceChat client
			//construction.Services.AddSingleton<ServiceChatClient>();

			// Return the construction for chaining
			return construction;
		}

		/// <summary>
		/// Injects the ChatClient application services
		/// </summary>
		/// <param name="construction"></param>
		/// <returns></returns>
		public static FrameworkConstruction AddChatClientServices(this FrameworkConstruction construction)
		{
			// Add our task manager
			construction.Services.AddTransient<ITaskManager, BaseTaskManager>();

			// Bind a file manager
			construction.Services.AddTransient<IFileManager, BaseFileManager>();

			// Bind a UI Manager
			construction.Services.AddTransient<IUIManager, UIManager>();

			// Return the construction for chaining
			return construction;
		}
	}
}
