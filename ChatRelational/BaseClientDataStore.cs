using Chat.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRelational
{
	/// <summary>
	/// Stores and retrieves information about the client application 
	/// such as login credentials, messages, settings and so on
	/// in an SQLite database
	/// </summary>
	public class BaseClientDataStore : IClientDataStore
	{
		#region Protected Members

		/// <summary>
		/// The database context for the client data store
		/// </summary>
		protected DataStoreDbContext mDbContext;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="dbContext">The database to use</param>
		public BaseClientDataStore(DataStoreDbContext dbContext)
		{
			mDbContext = dbContext;
		}

		#endregion

		#region Interface Implementation

		/// <summary>
		/// Makes sure the client data store is correctly set up
		/// </summary>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		public async Task EnsureDataStoreAsync()
		{
			// Make sure the database exists and is created
			await mDbContext.Database.EnsureCreatedAsync();
		}

		public Task<UserProfileDetailsApiModel> GetUserProfileDetailsAsync(LoginCredentialsApiModel loginCredentials)
		{
			// Find user data
			UserDataModel userCredentials = mDbContext.Users.First((user) => user.Username == loginCredentials.Username && user.Password == loginCredentials.Password);

			// Pass back the user details
			return Task.FromResult(new UserProfileDetailsApiModel
			{
				ID = userCredentials.Id,
				FirstName = userCredentials.FirstName,
				LastName = userCredentials.LastName,
				Username = userCredentials.Username,
			});
		}
		
		public async Task<UserProfileDetailsApiModel> AddNewUserAsync(RegisterCredentialsApiModel registerCredentials)
		{
			// Find user data
			UserDataModel userCredentials = mDbContext.Users.FirstOrDefault(user => user.Username == registerCredentials.Username && user.Password == registerCredentials.Password);

			// If user can be found
			if (userCredentials != null)
			{
				return null;
			}

			// If we get here, we are have not this user
			userCredentials = new UserDataModel()
			{
				Id = mDbContext.Users.Count(),
				FirstName = registerCredentials.FirstName,
				LastName = registerCredentials.LastName,
				Username = registerCredentials.Username,
				Password = registerCredentials.Password
			};

			// Add new one
			mDbContext.Users.Add(userCredentials);

			// Save changes
			await mDbContext.SaveChangesAsync();

			// Pass back the user details
			return new UserProfileDetailsApiModel
			{
				ID = userCredentials.Id,
				FirstName = userCredentials.FirstName,
				LastName = userCredentials.LastName,
				Username = userCredentials.Username,
			};
		}
		
		public async Task UpdateUserProfileDetailsAsync(UserProfileDetailsApiModel userProfileDetails)
		{
			// Find user data
			UserDataModel userCredentials = mDbContext.Users.First((user) => user.Id == userProfileDetails.ID);

			// Clear all entries
			mDbContext.Users.Remove(userCredentials);

			// Save changes
			await mDbContext.SaveChangesAsync();

			userCredentials.Username = userProfileDetails.Username;
			userCredentials.FirstName = userProfileDetails.FirstName;
			userCredentials.LastName = userProfileDetails.LastName;

			// Add new one
			mDbContext.Users.Add(userCredentials);

			// Save changes
			await mDbContext.SaveChangesAsync();
		}
		
		public async Task<List<ChatDataModel>> GetListOfChatsAsync(UserProfileDetailsApiModel userProfile)
		{
			// Search all chat for given user
			var chatsIds = mDbContext.Roster.Where(roster => roster.UserId == userProfile.ID).Select(user => user.ChatId);
			var chats = mDbContext.Chats.Where(chat => chatsIds.Contains(chat.Id));

			// Pass back the user details
			return chats?.ToList();
		}

		public async Task<List<MessageDataModel>> GetMessagesForChatAsync(int chatId)
		{
			// Search all messages for given chat id
			var messages = mDbContext.Messages.Where(message => message.ChatId == chatId);

			// Pass back the user details
			return messages?.ToList();
		}

		public async Task UpdateChatMessageStatusAsync(int userId, int messageId)
		{
			// Search all messages for given chat id
			MessageStatusDataModel message = mDbContext.MessagesStatus.First(message => message.MessageId == messageId && message.UserId == userId);

			// Clear all entries
			mDbContext.MessagesStatus.Remove(message);

			// Read message
			message.IsRead = true;

			// Add new one
			mDbContext.MessagesStatus.Add(message);

			// Save changes
			await mDbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Removes all data stored in the data store
		/// </summary>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		public async Task ClearAllDataAsync()
		{
			// Clear all data
			//mDbContext.Chats.RemoveRange(mDbContext.Chats);
			//mDbContext.Messages.RemoveRange(mDbContext.Messages);
			//mDbContext.MessagesStatus.RemoveRange(mDbContext.MessagesStatus);
			//mDbContext.Roster.RemoveRange(mDbContext.Roster);
			mDbContext.Users.RemoveRange(mDbContext.Users);

			// Save changes
			await mDbContext.SaveChangesAsync();
		}

		#endregion
	}
}