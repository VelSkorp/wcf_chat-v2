using Dna;
using Chat.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRelational
{
	/// <summary>
	/// Extension methods for the <see cref="FrameworkConstruction"/>
	/// </summary>
	public static class FrameworkConstructionExtensions
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public static FrameworkConstruction AddDataStore(this FrameworkConstruction construction)
		{
			// Inject our SQLite EF data store
			construction.Services.AddDbContext<DataStoreDbContext>(options =>
			{
				// Setup connection string
				options.UseSqlite(construction.Configuration.GetConnectionString("DataStoreConnection"));
			}, contextLifetime: ServiceLifetime.Transient);

			// Add client data store for easy access/use of the backing data store
			// Make it scoped so we can inject the scoped DbContext
			construction.Services.AddTransient<IDataStore>(
				provider => new BaseClientDataStore(provider.GetService<DataStoreDbContext>()));

			// Return framework for chaining
			return construction;
		}
	}
}