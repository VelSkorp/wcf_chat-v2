using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Core
{
	/// <summary>
	/// Stores and retrieves information about the client application 
	/// such as login credentials, messages, settings and so on
	/// </summary>
	public interface IDataStore
	{
		/// <summary>
		/// Makes sure the client data store is correctly set up
		/// </summary>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		Task EnsureDataStoreAsync();

		/// <summary>
		/// Gets the stored user profile details for this client
		/// </summary>
		/// <param name="loginCredentials">User credentials for logging in</param>
		/// <returns>Returns the login credentials if they exist, or throws exception if none exist</returns>
		/// <exception cref="System.InvalidOperationException"></exception>
		Task<UserProfileDetailsApiModel> GetUserProfileDetailsAsync(LoginCredentialsApiModel loginCredentials);

		/// <summary>
		/// Gets the stored chats for user
		/// </summary>
		/// <returns></returns>
		/// <param name="userProfile">User profile details</param>
		/// <returns>List of chats for user</returns>
		Task<List<ChatDataModel>> GetListOfChatsAsync(UserProfileDetailsApiModel userProfile);

		/// <summary>
		/// Gets the stored messages in chat
		/// </summary>
		/// <param name="chat">Chat data</param>
		/// <returns>List of messages for given chat</returns>
		Task<List<MessageDataModel>> GetMessagesForChatAsync(ChatDataModel chat);

		/// <summary>
		/// Gets the stored message status in chat for given user
		/// </summary>
		/// <param name="message">Message data</param>
		/// <returns>Message status</returns>
		/// <exception cref="System.InvalidOperationException"></exception>
		Task<MessageStatusDataModel> GetMessageStatusAsync(MessageDataModel message);

		/// <summary>
		/// Adds new user
		/// </summary>
		/// <param name="registerCredentials">User credentials for registration</param>
		/// <returns>Returns the user profile details if user successfully registered, or null if already exist</returns>
		Task<UserProfileDetailsApiModel> AddNewUserAsync(RegisterCredentialsApiModel registerCredentials);

		/// <summary>
		/// Adds new chat for this user and other users in this chat
		/// </summary>
		/// <param name="chat">Chat data</param>
		/// <param name="users">Users in this chat data</param>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		Task AddNewChatAsync(ChatDataModel chat, List<UserProfileDetailsApiModel> users);

		/// <summary>
		/// Adds new message in chat for this user and other users in this chat
		/// </summary>
		/// <param name="message">Message data</param>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		Task AddNewMessageAsync(MessageDataModel message);

		/// <summary>
		/// Updates information of user
		/// </summary>
		/// <param name="userProfile">New information about the user</param>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		/// <exception cref="System.InvalidOperationException"></exception>
		Task UpdateUserProfileDetailsAsync(UserProfileDetailsApiModel userProfile);

		/// <summary>
		/// Updates chat message status if it has been read
		/// </summary>
		/// <param name="message">Message that was read</param>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		/// <exception cref="System.InvalidOperationException"></exception>
		Task UpdateChatMessageStatusAsync(MessageDataModel message);

		/// <summary>
		/// Removes all data stored in the data store
		/// </summary>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		Task ClearAllDataAsync();
	}
}