using System;
using System.Windows;
using System.ServiceModel;
using System.Windows.Input;
using ChatClient.ServiceChat;
using System.IO;
using System.Text;

namespace ChatClient
{
    public partial class MainWindow : Window, IServiceChatCallback
    {
        ServiceChatClient client;
        int ID;
        Login login;
        public MainWindow()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        {
            ConnectUser();
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;            
        }
        void ConnectUser()
        {
            login = new Login();
            client = new ServiceChatClient(new InstanceContext(this));
            login.ShowDialog();

            do
            {
                try
                {
                    if (login.condition == 1) Registration();

                    if (login.condition == 0) ID = client.Connect(login.login, login.password);

                    if (ID == -1)
                    {
                        login = new Login("Этот пользователь не зарегистрирован", "Alert");
                        login.ShowDialog();
                    }

                    if (ID == -2)
                    {
                        login = new Login("этот пользователь уже в чате", "Alert");
                        login.ShowDialog();
                    }

                    if (login.condition == -1) Application.Current.Shutdown();

                }
                catch (EndpointNotFoundException)
                {
                    login = new Login("Не удалось подключится к серверу", "Alert");
                    //client = new ServiceChatClient(new InstanceContext(this));
                    login.ShowDialog();
                }
                catch (FaultException)
                {
                    login = new Login("Сервер недоступен", "Alert");
                    //client = new ServiceChatClient(new InstanceContext(this));
                    login.ShowDialog();
                }
                catch (ProtocolException)
                {
                    //TODO реализовать поддержку eception
                }
            } while (ID <= 0 & login.condition == 0);
        }

        private void Registration()
        {
            Register reg = new Register();
            bool? result=null;

            do
            {
                if (reg.ShowDialog() == false) Application.Current.Shutdown();
                else if (client.Register(reg.login, reg.password))
                {
                    login = new Login("Вы успешно зарегистрированны", "Success");
                    login.ShowDialog();
                    result = false;
                }
                else
                {
                    reg = new Register("Этот пользователь уже зарегистрирован", "Falue");
                    result = true;
                }
            } while (result==true);
        }

        void DisconnectUser()
        {
            if (ID!=0)
            {
                try
                {
                    client.Disconnect(ID);
                    client = null;
                }
                catch (Exception)
                {
                    //TODO обработать exception
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DisconnectUser();
            Application.Current.Shutdown();
        }

        public void MsgCallBack(string msg)
        {
            lbChat.Items.Add(msg);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count-1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (client != null)
                DisconnectUser(); 
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                if (client!=null)
                {
                    client.SendMsg(tbMessage.Text, ID);
                    tbMessage.Text = string.Empty;
                }                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Today;
            string filePath = @".\" + date.ToString("d")+".txt";
            FileStream strem = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            byte[] data;
            

            foreach (string item in lbChat.Items)
            {
                data = Encoding.UTF8.GetBytes(item+"\n");
                strem.Write(data, 0, data.Length);
            }

            MessageBox.Show("История успешно сохранена", "Success", MessageBoxButton.OKCancel,MessageBoxImage.Information);

            strem.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //TODO Open story
        }
    }
}