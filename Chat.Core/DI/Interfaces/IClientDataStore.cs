using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Core
{
	/// <summary>
	/// Stores and retrieves information about the client application 
	/// such as login credentials, messages, settings and so on
	/// </summary>
	public interface IClientDataStore
	{
		/// <summary>
		/// Makes sure the client data store is correctly set up
		/// </summary>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		Task EnsureDataStoreAsync();

		/// <summary>
		/// Gets the stored login credentials for this client
		/// </summary>
		/// <param name="loginCredentials">User credentials for logging in</param>
		/// <returns>Returns the login credentials if they exist, or null if none exist</returns>
		Task<UserProfileDetailsApiModel> GetUserProfileDetailsAsync(LoginCredentialsApiModel loginCredentials);

		/// <summary>
		/// Adds new login credentials for this client
		/// </summary>
		/// <param name="registerCredentials">User credentials for registration</param>
		/// <returns>Returns the login credentials if they exist, or null if none exist</returns>
		Task<UserProfileDetailsApiModel> AddNewUserAsync(RegisterCredentialsApiModel registerCredentials);

		/// <summary>
		/// Updates information of user
		/// </summary>
		/// <param name="loginCredentials">New information about the user</param>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		Task UpdateUserProfileDetailsAsync(UserProfileDetailsApiModel loginCredentials);

		/// <summary>
		/// Gets the stored chats for user
		/// </summary>
		/// <param name="userId">User id</param>
		/// <returns>List of chats for user</returns>
		Task<List<ChatDataModel>> GetListOfChatsAsync(UserProfileDetailsApiModel user);

		/// <summary>
		/// Gets the stored messages in chat
		/// </summary>
		/// <param name="chatId">Chat id</param>
		/// <returns>List of messages for chat</returns>
		Task<List<MessageDataModel>> GetMessagesForChatAsync(int chatId);

		/// <summary>
		/// Updates chat message status if it has been read
		/// </summary>
		/// <param name="chatId"></param>
		/// <param name="messageId"></param>
		/// <returns></returns>
		Task UpdateChatMessageStatusAsync(int chatId, int messageId);

		/// <summary>
		/// Removes all data stored in the data store
		/// </summary>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		Task ClearAllDataAsync();
	}
}