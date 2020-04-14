﻿using System.ServiceModel;

namespace wcf_chat
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
        void SendMsg(string msg,int id);
    }

    public interface IServiceChatCallBack
    {
        [OperationContract(IsOneWay =true)]
        void MsgCallBack(string msg);
    }
}