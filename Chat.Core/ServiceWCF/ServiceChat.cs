using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Chat.Core
{
	[ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
	public class ServiceChat : IServiceChat
	{
		private List<UserSarver> mUsers = new List<UserSarver>();
		private static ServiceChat Instance;
		private int mNextID = 1;

		public List<UserSarver> Users => mUsers;

		protected ServiceChat() { }

		public int Connect(string name,string password)
		{
			if (IsRegistered(name,password))
			{
				var user = new UserSarver
				{
					ID = mNextID,
					Name = name,
					Password = password,
					OperationContext = OperationContext.Current
				};

				if (!mUsers.Contains(mUsers.FirstOrDefault(i => i.Name == name)))
				{
					mNextID++;
					SendGeneralMsg(": " + user.Name + " подключился к чату", 0);
					mUsers.Add(user);
					return user.ID; //зарегистрированн и подключен
				}
				else return -2; //зарегистрированн но уже есть в чате
			}
			return -1; //не зарегистирирован
		}

		public void Disconnect(int id)
		{
			var user = mUsers.FirstOrDefault(i => i.ID == id);

			if (user!=null)
			{
				mUsers.Remove(user);
				SendGeneralMsg(": "+user.Name + " отключился от чата",0);
			}
		}


		public void SendGeneralMsg(string msg, int id)
		{
			try
			{
				foreach (var item in mUsers)
				{
					var answer = DateTime.Now.ToShortTimeString();
					var user = mUsers.FirstOrDefault(i => i.ID == id);

					if (user != null) answer += " " + user.Name + ": ";

					answer += msg;
					item.OperationContext.GetCallbackChannel<IServiceChatCallBack>().MsgCallBack(answer);
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
			if (!IsRegistered(name, password))
			{
				//TODO рализовать работу с бд
				var BDFile = new FileInfo(@"./BDFile.txt");
				var streamWriter = BDFile.AppendText();
				
				streamWriter.Write(Encoding.UTF8.GetBytes( name + ":" + password + "\n"));
				streamWriter.Close();
				return true;
			}
			return false;
			#endregion
		}

		private bool IsRegistered(string name, string password)
		{
			//TODO рализовать работу с бд

			var BDFile = new FileInfo(@"./BDFile.txt");
			var FileStream = BDFile.Open(FileMode.Open, FileAccess.Read);
			var byteData = new byte[FileStream.Length];

			FileStream.Read(byteData, 0, byteData.Length);
			FileStream.Close();

			var stringBuilder = new StringBuilder();

			stringBuilder.Append(Encoding.UTF8.GetChars(byteData));

			var regex = new Regex(@"([а-яА-Яa-zA-Z0-9]+):([a-zA-Z0-9]+)[^$]");
			var matches = regex.Matches(stringBuilder.ToString());

			foreach (Match match in matches)
				if (name == match.Groups[1].Value & password == match.Groups[2].Value) return true;

			return false;
		}

		public static ServiceChat GetInstance()
		{
			if (Instance == null) Instance = new ServiceChat();
			return Instance;
		}
	}
}