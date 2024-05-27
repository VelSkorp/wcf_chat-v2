using Dna;
using Chat.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ChatHostConsole
{
	/// <summary>
	/// Extension methods for the <see cref="FrameworkConstruction"/>
	/// </summary>
	public static class FrameworkConstructionExtensions
	{

		/// <summary>
		/// Injects the application services needed
		/// for the WCF Chat Server application
		/// </summary>
		/// <param name="construction"></param>
		/// <returns></returns>
		public static FrameworkConstruction AddHostServices(this FrameworkConstruction construction)
		{
			// Add our task manager
			construction.Services.AddTransient<ITaskManager, BaseTaskManager>();

			// Bind a file manager
			construction.Services.AddTransient<IFileManager, BaseFileManager>();

			// Return the construction for chaining
			return construction;
		}
	}
}