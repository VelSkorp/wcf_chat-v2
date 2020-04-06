using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wcf_chat;

namespace ChatHostWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
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
                FileInfo LogFile = new FileInfo(@"./Log.txt");
                StreamWriter streamWriter = LogFile.CreateText();
                streamWriter.Write(ex.Message);
                streamWriter.Close();
            }
        }

        private void bUserDiscon_Click(object sender, RoutedEventArgs e)
        {

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
