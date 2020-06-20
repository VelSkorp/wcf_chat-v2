using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using wcf_chat;

namespace ChatHostWPF
{
	public partial class MainWindow : Window
	{
		private ServiceHost mHost;
		public MainWindow()
		{
			InitializeComponent();
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			mHost = new ServiceHost(ServiceChat.GetInstance());
		}

		private void BOnOffServer_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (mHost.State==CommunicationState.Closed)
					mHost = new ServiceHost(typeof(ServiceChat));

				if (mHost.State == CommunicationState.Created)
				{
					mHost.Open();
					lbServerState.Items.Add("Хост стартовал");
					lbServerState.ScrollIntoView(lbServerState.Items[lbServerState.Items.Count - 1]);
					bOnOffServer.Content = "Выключить сервер";
				}
				else if (mHost.State == CommunicationState.Opened)
				{
					mHost.Close();
					lbServerState.Items.Add("Хост отключен");
					lbServerState.ScrollIntoView(lbServerState.Items[lbServerState.Items.Count - 1]);
					bOnOffServer.Content = "Запустить сервер";
				}
			}
			catch (Exception ex)
			{
				lbServerState.Items.Add(ex.Message);
				lbServerState.ScrollIntoView(lbServerState.Items[lbServerState.Items.Count - 1]);

				var date = DateTime.Today;
				var LogFile = new FileInfo(@"./Log" + date.ToString("d") + ".txt");
				var streamWriter = LogFile.CreateText();
				
				streamWriter.Write(ex.Message);
				streamWriter.Close();
			}
		}

		private void BUserDiscon_Click(object sender, RoutedEventArgs e)
		{
			if (lbActiveUsers.SelectedItem != null)
			{
				var userName = lbActiveUsers.SelectedItem.ToString();
				var service = mHost.SingletonInstance as ServiceChat;
				var user = service.Users.FirstOrDefault(i => i.Name == userName);

				service.SendGeneralMsg("Был отключен от чата сервером", user.ID);
				service.Disconnect(user.ID);
				UsersRefresh();
			}
		}

		private void BUsersRefresh_Click(object sender, RoutedEventArgs e)
		{
			UsersRefresh();
		}

		private void UsersRefresh()
		{
			lbActiveUsers.Items.Clear();

			var service = mHost.SingletonInstance as ServiceChat;
			var users = service.Users;

			foreach (var user in users)
				lbActiveUsers.Items.Add(user.Name);
		}

		private void BOnOffBot_Click(object sender, RoutedEventArgs e)
		{
			//TODO придумать и реализовать вохможности бота


		}
	}
}