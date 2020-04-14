﻿using System;
using System.IO;
using System.ServiceModel;
using System.Windows;
using wcf_chat;

namespace ChatHostWPF
{
    public partial class MainWindow : Window
    {
        ServiceHost host;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            host = new ServiceHost(ServiceChat.Instance());
        }

        private void bOnOffServer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (host.State==CommunicationState.Closed)
                    host = new ServiceHost(typeof(ServiceChat));

                if (host.State == CommunicationState.Created)
                {
                    host.Open();
                    lbServerState.Items.Add("Хост стартовал");
                    lbServerState.ScrollIntoView(lbServerState.Items[lbServerState.Items.Count - 1]);
                    bOnOffServer.Content = "Выключить сервер";
                }
                else if (host.State == CommunicationState.Opened)
                {
                    host.Close();
                    lbServerState.Items.Add("Хост отключен");
                    lbServerState.ScrollIntoView(lbServerState.Items[lbServerState.Items.Count - 1]);
                    bOnOffServer.Content = "Запустить сервер";
                }
            }
            catch (Exception ex)
            {
                lbServerState.Items.Add(ex.Message);
                lbServerState.ScrollIntoView(lbServerState.Items[lbServerState.Items.Count - 1]);

                DateTime date = DateTime.Today;
                FileInfo LogFile = new FileInfo(@"./Log"+date.ToString("d")+".txt");
                StreamWriter streamWriter = LogFile.CreateText();

                streamWriter.Write(ex.Message);
                streamWriter.Close();
            }
        }

        private void bUserDiscon_Click(object sender, RoutedEventArgs e)
        {
            //TODO user disconnect
        }

        private void bUsersRefresh_Click(object sender, RoutedEventArgs e)
        {
            lbActiveUsers.Items.Clear();

            var service = host.SingletonInstance as ServiceChat;
            var users = service.Users;

            foreach (var user in users)
                lbActiveUsers.Items.Add(user.Name);
        }
    }
}