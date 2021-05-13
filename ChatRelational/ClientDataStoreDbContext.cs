using Chat.Core;
using Microsoft.EntityFrameworkCore;

namespace ChatRelational
{
	/// <summary>
	/// The database context for the client data store
	/// </summary>
	public class ClientDataStoreDbContext : DbContext
	{
		#region DbSets 

		/// <summary>
		/// The chats details
		/// </summary>
		public DbSet<ChatDataModel> Chats { get; set; }

		/// <summary>
		/// The messages status
		/// </summary>
		public DbSet<MessageStatusDataModel> MessagesStatus { get; set; }

		/// <summary>
		/// The messages details
		/// </summary>
		public DbSet<MessagesDataModel> Messages { get; set; }

		/// <summary>
		/// The users in the same chat
		/// </summary>
		public DbSet<PartyDataModel> Party { get; set; }

		/// <summary>
		/// The user profiles details
		/// </summary>
		public DbSet<UserDataModel> Users { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ClientDataStoreDbContext(DbContextOptions<ClientDataStoreDbContext> options) : base(options) { }

		#endregion

		#region Model Creating

		/// <summary>
		/// Configures the database structure and relationships
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Fluent API

			// Configure tables
			// --------------------------
			//
			// Set primary keys
			modelBuilder.Entity<ChatDataModel>().HasKey(a => a.ID);
			modelBuilder.Entity<MessagesDataModel>().HasKey(a => a.ID);
			modelBuilder.Entity<UserDataModel>().HasKey(a => a.ID);
		}

		#endregion
	}
}