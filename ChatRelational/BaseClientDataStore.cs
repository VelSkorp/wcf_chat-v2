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
	public class BaseClientDataStore : IDataStore
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
				Id = userCredentials.Id,
				FirstName = userCredentials.FirstName,
				LastName = userCredentials.LastName,
				Username = userCredentials.Username,
			});
		}

		public Task<List<ChatDataModel>> GetListOfChatsAsync(UserProfileDetailsApiModel userProfile)
		{
			// Search all chat for given user
			var chatsIds = mDbContext.Roster.Where(roster => roster.UserId == userProfile.Id).Select(user => user.ChatId);
			var chats = mDbContext.Chats.Where(chat => chatsIds.Contains(chat.Id));

			// Pass back the user details
			return Task.FromResult(chats.ToList());
		}

		public Task<MessageStatusDataModel> GetMessageStatusAsync(MessageDataModel message)
		{
			// Search message status for given message
			var messages = mDbContext.MessagesStatus.First(messagesStatus => messagesStatus.MessageId == message.Id && messagesStatus.UserId == message.UserId);

			// Pass back the user details
			return Task.FromResult(messages);
		}

		public Task<List<MessageDataModel>> GetMessagesForChatAsync(ChatDataModel chat)
		{
			// Search all messages for given chat id
			var messages = mDbContext.Messages.Where(message => message.ChatId == chat.Id);

			// Pass back the user details
			return Task.FromResult(messages.ToList());
		}

		public Task<UserProfileDetailsApiModel> AddNewUserAsync(RegisterCredentialsApiModel registerCredentials)
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
				Id = mDbContext.Users.Count() + 1,
				FirstName = registerCredentials.FirstName,
				LastName = registerCredentials.LastName,
				Username = registerCredentials.Username,
				Password = registerCredentials.Password
			};

			// Add new one
			mDbContext.Users.Add(userCredentials);

			// Save changes
			mDbContext.SaveChangesAsync();

			// Pass back the user details
			return Task.FromResult(new UserProfileDetailsApiModel
			{
				Id = userCredentials.Id,
				FirstName = userCredentials.FirstName,
				LastName = userCredentials.LastName,
				Username = userCredentials.Username,
			});
		}

		public async Task AddNewChatAsync(ChatDataModel chat, List<UserProfileDetailsApiModel> users)
		{
			// If chat can be found
			if (mDbContext.Chats.Contains(chat))
			{
				return;
			}

			// Add new one
			mDbContext.Chats.Add(chat);

			// Save changes
			await mDbContext.SaveChangesAsync();

			// Connect users and chats
			users.ForEach(user => mDbContext.Roster.Add(new RosterDataModel()
			{
				ChatId = chat.Id,
				UserId = user.Id
			}));

			// Save changes
			await mDbContext.SaveChangesAsync();
		}

		public async Task AddNewMessageAsync(MessageDataModel message)
		{
			// If chat can be found
			if (mDbContext.Messages.Contains(message))
			{
				return;
			}

			var messageStatus = new MessageStatusDataModel()
			{
				MessageId = message.Id,
				UserId = message.UserId
			};

			// Add new one
			mDbContext.Messages.Add(message);
			mDbContext.MessagesStatus.Add(messageStatus);

			// Save changes
			await mDbContext.SaveChangesAsync();
		}

		public async Task UpdateUserProfileDetailsAsync(UserProfileDetailsApiModel userProfile)
		{
			// Find user data
			UserDataModel userCredentials = mDbContext.Users.First((user) => user.Id == userProfile.Id);

			// Clear all entries
			mDbContext.Users.Remove(userCredentials);

			// Save changes
			await mDbContext.SaveChangesAsync();

			userCredentials.Username = userProfile.Username;
			userCredentials.FirstName = userProfile.FirstName;
			userCredentials.LastName = userProfile.LastName;

			// Add new one
			mDbContext.Users.Add(userCredentials);

			// Save changes
			await mDbContext.SaveChangesAsync();
		}

		public async Task UpdateChatMessageStatusAsync(MessageDataModel message)
		{
			// Search all messages for given chat id
			MessageStatusDataModel messageStatus = mDbContext.MessagesStatus.First(message => message.MessageId == message.Id && message.UserId == message.UserId);

			// Clear all entries
			mDbContext.MessagesStatus.Remove(messageStatus);

			// Read message
			messageStatus.IsRead = true;

			// Add new one
			mDbContext.MessagesStatus.Add(messageStatus);

			// Save changes
			await mDbContext.SaveChangesAsync();
		}

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