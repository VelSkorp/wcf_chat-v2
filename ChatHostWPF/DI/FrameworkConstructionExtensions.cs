﻿using Dna;
using ChatHost.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ChatHostWPF
{
	/// <summary>
	/// Extension methods for the <see cref="FrameworkConstruction"/>
	/// </summary>
	public static class FrameworkConstructionExtensions
	{
		/// <summary>
		/// Injects the view models needed for Fasetto Word application
		/// </summary>
		/// <param name="construction"></param>
		/// <returns></returns>
		public static FrameworkConstruction AddChatHostViewModels(this FrameworkConstruction construction)
		{
			// Bind to a single instance of Application view model
			construction.Services.AddSingleton<ApplicationViewModel>();

			// Return the construction for chaining
			return construction;
		}

		/// <summary>
		/// Injects the Fasetto Word client application services needed
		/// for the Fasetto Word application
		/// </summary>
		/// <param name="construction"></param>
		/// <returns></returns>
		public static FrameworkConstruction AddChatClientHostServices(this FrameworkConstruction construction)
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