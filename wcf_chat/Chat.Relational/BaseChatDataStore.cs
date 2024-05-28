using Chat.Core;
using Microsoft.EntityFrameworkCore;
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
	public class BaseChatDataStore : IDataStore
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
		public BaseChatDataStore(DataStoreDbContext dbContext)
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

		public async Task<UserProfileDetailsApiModel> GetUserProfileDetailsAsync(string username)
		{
			// Find user data
			var userCredentials = await mDbContext.Users.AsNoTracking().Where(user => user.Username.Equals(username)).FirstOrDefaultAsync();

			// Pass back the user details
			return new UserProfileDetailsApiModel
			{
				Id = userCredentials.Id,
				FirstName = userCredentials.FirstName,
				LastName = userCredentials.LastName,
				Username = userCredentials.Username,
			};
		}

		public async Task<bool> LoginUserAsync(LoginCredentialsApiModel loginCredentials)
		{
			// Find user data
			var user = await mDbContext.Users.AsNoTracking().Where(user => user.Username.Equals(loginCredentials.Username)
				&& user.Password.Equals(loginCredentials.Password)).FirstOrDefaultAsync();

			return user is not null;
		}

		public async Task<List<ChatDataModel>> GetListOfChatsAsync(UserProfileDetailsApiModel userProfile)
		{
			// Search all chat for given user
			var chatsIds = await mDbContext.Roster.AsNoTracking().Where(roster => roster.UserId == userProfile.Id).Select(user => user.ChatId).ToListAsync();
			
			// Pass back the chats details
			return await mDbContext.Chats.AsNoTracking().Where(chat => chatsIds.Contains(chat.Id)).ToListAsync();
		}

		public async Task<MessageStatusDataModel> GetMessageStatusAsync(MessageDataModel message)
		{
			// Search and pass back message status for given message
			return await mDbContext.MessagesStatus.AsNoTracking().FirstOrDefaultAsync(messageStatus => messageStatus.MessageId == message.Id && messageStatus.UserId == message.UserId);
		}

		public async Task<List<MessageDataModel>> GetMessagesForChatAsync(ChatDataModel chat)
		{
			// Search and pass back all messages for given chat id
			return await mDbContext.Messages.AsNoTracking().Where(message => message.ChatId == chat.Id).ToListAsync();
		}

		public async Task<bool> RegisterUserAsync(RegisterCredentialsApiModel registerCredentials)
		{
			// Find user data
			var userCredentials = await mDbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Username.Equals(registerCredentials.Username) && user.Password.Equals(registerCredentials.Password));

			// If user can be found
			if (userCredentials is not null)
			{
				return false;
			}

			// If we get here, we are have not this user
			userCredentials = new UserDataModel()
			{
				Id = await mDbContext.Users.AsNoTracking().CountAsync() + 1,
				FirstName = registerCredentials.FirstName,
				LastName = registerCredentials.LastName,
				Username = registerCredentials.Username,
				Password = registerCredentials.Password
			};

			// Add new one
			await mDbContext.Users.AddAsync(userCredentials);

			// Save changes
			return await mDbContext.SaveChangesAsync() == 1;
		}

		public async Task<bool> AddNewChatAsync(ChatDataModel chat, List<UserProfileDetailsApiModel> users)
		{
			// If chat can be found
			if (await mDbContext.Chats.ContainsAsync(chat))
			{
				return false;
			}

			// Set next id for chat
			chat.Id = await mDbContext.Chats.AsNoTracking().CountAsync() + 1;

			// Add new one
			await mDbContext.Chats.AddAsync(chat);

			// Save changes
			var isChatSaved = await mDbContext.SaveChangesAsync() == 1;

			// Connect users and chats
			var connections = new List<RosterDataModel>();
			users.ForEach(user => connections.Add(new RosterDataModel()
			{
				ChatId = chat.Id,
				UserId = user.Id
			}));

			// Add users and chats connections
			await mDbContext.Roster.AddRangeAsync(connections);

			// Save changes
			return isChatSaved && await mDbContext.SaveChangesAsync() == 1;
		}

		public async Task<bool> AddNewMessageAsync(MessageDataModel message)
		{
			// If chat can be found
			if (await mDbContext.Messages.ContainsAsync(message))
			{
				return false;
			}

			// Set next id for message
			message.Id = await mDbContext.Messages.CountAsync() + 1;

			// Add new one
			await mDbContext.Messages.AddAsync(message);

			// Save changes
			var isMessageSaved = await mDbContext.SaveChangesAsync() == 1;

			// Get all users in chat
			var usersInChat = await mDbContext.Roster.Where(roster => roster.ChatId == message.ChatId && roster.UserId != message.UserId).ToListAsync();

			// Connect users and messages
			var connections = new List<MessageStatusDataModel>();
			usersInChat.ForEach(user => connections.Add(new MessageStatusDataModel()
			{
				MessageId = message.Id,
				UserId = user.UserId
			}));

			// Add users and chats connections
			await mDbContext.MessagesStatus.AddRangeAsync(connections);

			// Read message for user who sent this message
			await mDbContext.MessagesStatus.AddAsync(new MessageStatusDataModel()
			{
				MessageId = message.Id,
				UserId = message.UserId,
				IsRead = true
			});

			// Save changes
			return isMessageSaved && await mDbContext.SaveChangesAsync() == 1;
		}

		public async Task<bool> UpdateUserProfileDetailsAsync(UserProfileDetailsApiModel userProfile)
		{
			// Find user data
			var userCredentials = await mDbContext.Users.SingleOrDefaultAsync(user => user.Id == userProfile.Id);

			// Update existing
			userCredentials.Username = userProfile.Username;
			userCredentials.FirstName = userProfile.FirstName;
			userCredentials.LastName = userProfile.LastName;

			// Save changes
			return await mDbContext.SaveChangesAsync() == 1;
		}

		public async Task<bool> UpdateChatMessageStatusAsync(MessageDataModel message)
		{
			// Search all messages for given chat id
			var messageStatus = await mDbContext.MessagesStatus.SingleOrDefaultAsync(messagesStatus => messagesStatus.MessageId == message.Id && messagesStatus.UserId == message.UserId);

			// Read message
			messageStatus.IsRead = true;

			// Save changes
			return await mDbContext.SaveChangesAsync() == 1;
		}

		public async Task<bool> ClearAllDataAsync()
		{
			// Clear all data cascade
			mDbContext.Users.RemoveRange(mDbContext.Users);

			// Save changes
			return await mDbContext.SaveChangesAsync() == 1;
		}

		#endregion
	}
}