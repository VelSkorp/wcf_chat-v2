using CoreWCF;
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
		Task<ApiResponse<UserProfileDetailsApiModel>> ConnectAsync(LoginCredentialsApiModel loginCredentials);

		/// <summary>
		/// Tries to register for a new account on the server
		/// </summary>
		/// <param name="registerCredentials">The registration details</param>
		/// <returns>Returns the result of the register request</returns>
		[OperationContract]
		Task<ApiResponse<UserProfileDetailsApiModel>> RegisterAsync(RegisterCredentialsApiModel registerCredentials);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userId">Current user id</param>
		/// <returns>Returns list of chats</returns>
		[OperationContract]
		Task<ApiResponse<List<ChatDataModel>>> GetChatsAsync();

		[OperationContract]
		void SendMessage(string message, int chatId);
	}
}