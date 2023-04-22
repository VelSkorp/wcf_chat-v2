using Chat.Core;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatRelational
{
	/// <summary>
	/// The database context for the client data store
	/// </summary>
	public class DataStoreDbContext : DbContext
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
		public DbSet<MessageDataModel> Messages { get; set; }

		/// <summary>
		/// The users in the same chat
		/// </summary>
		public DbSet<RosterDataModel> Roster { get; set; }

		/// <summary>
		/// The user profiles details
		/// </summary>
		public DbSet<UserDataModel> Users { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public DataStoreDbContext(DbContextOptions<DataStoreDbContext> options) : base(options) { }

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
			modelBuilder.Entity<ChatDataModel>().ToTable("Chats").HasKey(a => a.Id);
			modelBuilder.Entity<MessageDataModel>().ToTable("MessagesStatus").HasKey(a => a.Id);
			modelBuilder.Entity<MessageStatusDataModel>().ToTable("Messages").HasKey(a => a.Id);
			modelBuilder.Entity<RosterDataModel>().ToTable("Roster").HasKey(a => a.Id);
			modelBuilder.Entity<UserDataModel>().ToTable("Users").HasKey(a => a.Id);
		}

		#endregion
	}
}