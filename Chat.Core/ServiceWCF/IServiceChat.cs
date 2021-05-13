using System.ServiceModel;
using System.Threading.Tasks;

namespace Chat.Core
{
	[ServiceContract]
	public interface IServiceChat
	{
		[OperationContract]
		Task<ApiResponse<UserProfileDetailsApiModel>> ConnectAsync(LoginCredentialsApiModel loginCredentials);

		[OperationContract]
		bool Register(string name,string password);

		[OperationContract(IsOneWay =true)]
		void SendMessage(string message,int id);
	}
}