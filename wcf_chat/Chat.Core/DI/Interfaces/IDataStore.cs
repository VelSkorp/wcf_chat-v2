using System;
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
		/// Ensures the data store is created and available.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation.</returns>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		/// <exception cref="DbUpdateException">Thrown if there is an error updating the database.</exception>
		Task EnsureDataStoreAsync();

		/// <summary>
		/// Retrieves user profile details asynchronously by username.
		/// </summary>
		/// <param name="username">The username of the user to retrieve.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the user profile details.</returns>
		/// <exception cref="ArgumentNullException">Thrown if the username is null.</exception>
		/// <exception cref="InvalidOperationException">Thrown if the user is not found.</exception>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		Task<UserProfileDetailsApiModel> GetUserProfileDetailsAsync(string username);

		/// <summary>
		/// Retrieves a list of chats asynchronously for a given user profile.
		/// </summary>
		/// <param name="userProfile">The user profile details.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a list of chat data models.</returns>
		/// <exception cref="ArgumentNullException">Thrown if userProfile is null.</exception>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		Task<List<ChatDataModel>> GetListOfChatsAsync(UserProfileDetailsApiModel userProfile);

		/// <summary>
		/// Retrieves all messages for a given chat asynchronously.
		/// </summary>
		/// <param name="chat">The chat data model.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a list of message data models.</returns>
		/// <exception cref="ArgumentNullException">Thrown if chat is null.</exception>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		Task<List<MessageDataModel>> GetMessagesForChatAsync(ChatDataModel chat);

		/// <summary>
		/// Retrieves the status of a message asynchronously.
		/// </summary>
		/// <param name="message">The message data model.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the message status data model.</returns>
		/// <exception cref="ArgumentNullException">Thrown if message is null.</exception>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		Task<MessageStatusDataModel> GetMessageStatusAsync(MessageDataModel message);

		/// <summary>
		/// Logs in a user asynchronously using their login credentials.
		/// </summary>
		/// <param name="loginCredentials">The login credentials of the user.</param>
		/// <returns>A task that represents the asynchronous operation. The task result is true if the user is found; otherwise, false.</returns>
		/// <exception cref="ArgumentNullException">Thrown if loginCredentials is null.</exception>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		Task<bool> LoginUserAsync(LoginCredentialsApiModel loginCredentials);

		/// <summary>
		/// Registers a new user asynchronously.
		/// </summary>
		/// <param name="registerCredentials">The registration credentials of the user.</param>
		/// <returns>A task that represents the asynchronous operation. The task result is true if the user is registered; otherwise, false.</returns>
		/// <exception cref="ArgumentNullException">Thrown if registerCredentials is null.</exception>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		/// <exception cref="DbUpdateException">Thrown if there is an error updating the database.</exception>
		Task<bool> RegisterUserAsync(RegisterCredentialsApiModel registerCredentials);

		/// <summary>
		/// Adds a new chat asynchronously.
		/// </summary>
		/// <param name="chat">The chat data model.</param>
		/// <param name="users">The list of user profiles to associate with the chat.</param>
		/// <returns>A task that represents the asynchronous operation. The task result is true if the chat is added; otherwise, false.</returns>
		/// <exception cref="ArgumentNullException">Thrown if chat or users is null.</exception>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		/// <exception cref="DbUpdateException">Thrown if there is an error updating the database.</exception>
		Task<bool> AddNewChatAsync(ChatDataModel chat, List<UserProfileDetailsApiModel> users);

		/// <summary>
		/// Adds a new message asynchronously.
		/// </summary>
		/// <param name="message">The message data model.</param>
		/// <returns>A task that represents the asynchronous operation. The task result is true if the message is added; otherwise, false.</returns>
		/// <exception cref="ArgumentNullException">Thrown if message is null.</exception>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		/// <exception cref="DbUpdateException">Thrown if there is an error updating the database.</exception>
		Task<bool> AddNewMessageAsync(MessageDataModel message);

		/// <summary>
		/// Updates the user profile details asynchronously.
		/// </summary>
		/// <param name="userProfile">The user profile details to update.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		/// <exception cref="ArgumentNullException">Thrown if userProfile is null.</exception>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		/// <exception cref="DbUpdateException">Thrown if there is an error updating the database.</exception>
		Task<bool> UpdateUserProfileDetailsAsync(UserProfileDetailsApiModel userProfile);

		/// <summary>
		/// Updates the status of a message in a chat asynchronously.
		/// </summary>
		/// <param name="message">The message data model.</param>
		/// <returns>A task that represents the asynchronous operation. The task result is true if the message status is updated; otherwise, false.</returns>
		/// <exception cref="ArgumentNullException">Thrown if message is null.</exception>
		/// <exception cref="InvalidOperationException">Thrown if the message status is not found.</exception>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		/// <exception cref="DbUpdateException">Thrown if there is an error updating the database.</exception>
		Task<bool> UpdateChatMessageStatusAsync(MessageDataModel message);

		/// <summary>
		/// Clears all data from the data store asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result is true if the data is cleared; otherwise, false.</returns>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		/// <exception cref="DbUpdateException">Thrown if there is an error updating the database.</exception>
		Task<bool> ClearAllDataAsync();
	}
}