using CoreWCF;
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
		Task<ApiResponse<RegisterResultApiModel>> RegisterAsync(RegisterCredentialsApiModel registerCredentials);

		[OperationContract(IsOneWay =true)]
		void SendMessage(string message,int id);
	}
}