﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        private List<UserSarver> users = new List<UserSarver>();
        private static ServiceChat instance;
        public List<UserSarver> Users { get { return users; } }
        int NextID = 0;
        protected ServiceChat() { }
        public int Connect(string name)
        {
            UserSarver user = new UserSarver
            {
                ID = NextID,
                Name = name,
                operationContext = OperationContext.Current
            };
            NextID++;
            SendMsg(": "+user.Name + " подключился к чату",0);
            users.Add(user);
            return (user.ID);            
        }
        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user!=null)
            {
                users.Remove(user);
                SendMsg(": "+user.Name + " отключился от чата",0);
            }
        }
        public void SendMsg(string msg, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += " " + user.Name + ": ";
                }
                answer += msg;
                item.operationContext.GetCallbackChannel<IServiceChatCallBack>().MsgCallBack(answer);
            }
        }
        public static ServiceChat Instance()
        {
            if (instance == null) 
                instance = new ServiceChat();
            return instance;
        }
    }
}