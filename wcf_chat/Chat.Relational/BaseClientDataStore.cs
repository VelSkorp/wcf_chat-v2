using Chat.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Relational
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
			var userCredentials = mDbContext.Users.First((user) => user.Username == loginCredentials.Username && user.Password == loginCredentials.Password);

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
			var status = mDbContext.MessagesStatus.First(messageStatus => messageStatus.MessageId == message.Id && messageStatus.UserId == message.UserId);

			// Pass back the user details
			return Task.FromResult(status);
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
			var userCredentials = mDbContext.Users.FirstOrDefault(user => user.Username == registerCredentials.Username && user.Password == registerCredentials.Password);

			// If user can be found
			if (userCredentials != null)
			{
				throw new InvalidOperationException("User already exists in database");
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

			// Set next id for chat
			chat.Id = mDbContext.Chats.Count() + 1;

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

			// Set next id for message
			message.Id = mDbContext.Messages.Count() + 1;

			// Add new one
			mDbContext.Messages.Add(message);

			// Save changes
			await mDbContext.SaveChangesAsync();

			// Get all users in chat
			var usersInChat = mDbContext.Roster.Where(roster => roster.ChatId == message.ChatId && roster.UserId != message.UserId).ToList();

			// Connect users and messages
			usersInChat.ForEach(user => mDbContext.MessagesStatus.Add(new MessageStatusDataModel()
			{
				MessageId = message.Id,
				UserId = user.UserId
			}));

			// Read message for user who sent this message
			mDbContext.MessagesStatus.Add(new MessageStatusDataModel()
			{
				MessageId = message.Id,
				UserId = message.UserId,
				IsRead = true
			});

			// Save changes
			await mDbContext.SaveChangesAsync();
		}

		public async Task UpdateUserProfileDetailsAsync(UserProfileDetailsApiModel userProfile)
		{
			// Find user data
			var userCredentials = mDbContext.Users.First((user) => user.Id == userProfile.Id);

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
			var messageStatus = mDbContext.MessagesStatus.First(messagesStatus => messagesStatus.MessageId == message.Id && messagesStatus.UserId == message.UserId);

			// Clear all entries
			mDbContext.MessagesStatus.Remove(messageStatus);

			// Save changes
			await mDbContext.SaveChangesAsync();

			// Read message
			messageStatus.IsRead = true;

			// Add new one
			mDbContext.MessagesStatus.Add(messageStatus);

			// Save changes
			await mDbContext.SaveChangesAsync();
		}

		public async Task ClearAllDataAsync()
		{
			// Clear all data cascade
			mDbContext.Users.RemoveRange(mDbContext.Users);

			// Save changes
			await mDbContext.SaveChangesAsync();
		}

		#endregion
	}
}