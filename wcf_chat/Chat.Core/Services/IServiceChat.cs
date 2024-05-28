using System.ServiceModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Core
{
	[ServiceContract]
	public interface IServiceChat
	{
		/// <summary>
		/// Logs in a user
		/// </summary>
		/// <param name="loginCredentials">The login details</param>
		/// <returns>Returns the result of the login request</returns>
		[OperationContract]
		Task<ApiResponse> ConnectAsync(LoginCredentialsApiModel loginCredentials);

		/// <summary>
		/// Gets the stored chats for user
		/// </summary>
		/// <returns></returns>
		/// <param name="userProfile">User profile details</param>
		/// <returns>List of chats for user</returns>
		[OperationContract]
		Task<ApiResponse<UserProfileDetailsApiModel>> GetUserProfileDetailsAsync(string username);

		/// <summary>
		/// Gets the stored chats for user
		/// </summary>
		/// <returns></returns>
		/// <param name="userProfile">User profile details</param>
		/// <returns>List of chats for user</returns>
		[OperationContract]
		Task<ApiResponse<List<ChatDataModel>>> GetChatsForUserAsync(UserProfileDetailsApiModel userProfile);

		/// <summary>
		/// Gets the stored messages in chat
		/// </summary>
		/// <param name="chat">Chat data</param>
		/// <returns>List of messages for given chat</returns>
		[OperationContract]
		Task<ApiResponse<List<MessageDataModel>>> GetMessagesForChatAsync(ChatDataModel chat);

		/// <summary>
		/// Gets the stored message status in chat for given user
		/// </summary>
		/// <param name="message">Message data</param>
		/// <returns>Message status</returns>
		[OperationContract]
		Task<ApiResponse<MessageStatusDataModel>> GetMessageStatusAsync(MessageDataModel message);

		/// <summary>
		/// Tries to register for a new account on the server
		/// </summary>
		/// <param name="registerCredentials">The registration details</param>
		/// <returns>Returns the result of the register request</returns>
		[OperationContract]
		Task<ApiResponse> RegisterAsync(RegisterCredentialsApiModel registerCredentials);

		/// <summary>
		/// Adds new chat for this user and other users in this chat
		/// </summary>
		/// <param name="chat">Chat data</param>
		/// <param name="users">Users in this chat data</param>
		[OperationContract]
		Task<ApiResponse> CreateChatAsync(ChatDataModel chat, List<UserProfileDetailsApiModel> users);

		/// <summary>
		/// Sends message in chat for this user and other users in this chat
		/// </summary>
		/// <param name="message">Message data</param>
		[OperationContract]
		Task<ApiResponse> SendMessageAsync(MessageDataModel message);

		/// <summary>
		/// Updates information of user
		/// </summary>
		/// <param name="userProfile">New information about the user</param>
		[OperationContract]
		Task<ApiResponse> UpdateUserProfileDetailsAsync(UserProfileDetailsApiModel userProfile);

		/// <summary>
		/// Updates chat message status if it has been read
		/// </summary>
		/// <param name="message">Message that was read</param>
		[OperationContract]
		Task<ApiResponse> UpdateChatMessageStatusAsync(MessageDataModel message);
	}
}