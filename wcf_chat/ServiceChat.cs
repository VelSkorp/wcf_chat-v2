using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;

namespace wcf_chat
{
	[ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        private List<UserSarver> users = new List<UserSarver>();
        private static ServiceChat instance;
        private int NextID = 1;

        public List<UserSarver> Users { get { return users; } }

        protected ServiceChat() { }

        public int Connect(string name,string password)
        {
            if (isRegistered(name,password))
            {
                UserSarver user = new UserSarver
                {
                    ID = NextID,
                    Name = name,
                    Password = password,
                    operationContext = OperationContext.Current
                };

                if (!users.Contains(users.FirstOrDefault(i => i.Name == name)))
                {
                    NextID++;
                    SendGeneralMsg(": " + user.Name + " подключился к чату", 0);
                    users.Add(user);
                    return user.ID; //зарегистрированн и подключен
                }
                else return -2; //зарегистрированн но уже есть в чате
            }
            return -1; //не зарегистирирован
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);

            if (user!=null)
            {
                users.Remove(user);
                SendGeneralMsg(": "+user.Name + " отключился от чата",0);
            }
        }


        public void SendGeneralMsg(string msg, int id)
        {
            try
            {
                foreach (var item in users)
                {
                    string answer = DateTime.Now.ToShortTimeString();
                    var user = users.FirstOrDefault(i => i.ID == id);

                    if (user != null) answer += " " + user.Name + ": ";

                    answer += msg;
                    item.operationContext.GetCallbackChannel<IServiceChatCallBack>().MsgCallBack(answer);
                }
            }
            catch (CommunicationObjectAbortedException)
            {
                //System.ServiceModel.CommunicationObjectAbortedException: "Коммуникационный объект System.ServiceModel.Channels.ServiceChannel нельзя использовать для связи, так как его работа прервана."
            }
        }

        public void SendPrivateMsg(string msg, int id)
        {
            //TODO реализовать личное сообщение
        }

        public bool Register(string name, string password)
        {
            #region типа база дынных
            if (!isRegistered(name, password))
            {
                //TODO рализовать работу с бд
                FileInfo BDFile = new FileInfo(@"./BDFile.txt");
                StreamWriter streamWriter = BDFile.AppendText();
                
                streamWriter.Write(Encoding.UTF8.GetBytes( name + ":" + password + "\n"));
                streamWriter.Close();
                return true;
            }
            return false;
            #endregion
        }

        private bool isRegistered(string name, string password)
        {
            //TODO рализовать работу с бд

            FileInfo BDFile = new FileInfo(@"./BDFile.txt");
            FileStream FileStream = BDFile.Open(FileMode.Open, FileAccess.Read);
            byte[] byteData = new byte[FileStream.Length];

            FileStream.Read(byteData, 0, byteData.Length);
            FileStream.Close();

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(Encoding.UTF8.GetChars(byteData));

            Regex regex = new Regex(@"([а-яА-Яa-zA-Z0-9]+):([a-zA-Z0-9]+)[^$]");
            MatchCollection matches = regex.Matches(stringBuilder.ToString());

            foreach (Match match in matches)
                if (name == match.Groups[1].Value & password == match.Groups[2].Value) return true;

            return false;
        }

        public static ServiceChat Instance()
        {
            if (instance == null) instance = new ServiceChat();
            return instance;
        }
    }
}