using System.ServiceModel;

namespace Chat.Core
{
	[ServiceContract (CallbackContract =typeof(IServiceChatCallBack))]
	public interface IServiceChat
	{
		[OperationContract]
		int Connect(string name,string password);

		[OperationContract]
		bool Register(string name,string password);

		[OperationContract]
		void Disconnect(int ID);

		[OperationContract(IsOneWay =true)]
		void SendGeneralMsg(string msg,int id);

		[OperationContract(IsOneWay =true)]
		void SendPrivateMsg(string msg,int id);
	}

	public interface IServiceChatCallBack
	{
		[OperationContract(IsOneWay =true)]
		void MsgCallBack(string msg);
	}
}