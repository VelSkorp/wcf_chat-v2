using System;
using System.Windows;
using System.ServiceModel;
using System.Windows.Input;
using ChatClient.ServiceChat;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace ChatClient
{
    public partial class MainWindow : Window, IServiceChatCallback
    {
        ServiceChatClient client;
        int ID;
        LoginDel login;

        public MainWindow()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        {
            ConnectUser();
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;            
        }

        void ConnectUser()
        {
            login = new LoginDel();
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
                        login = new LoginDel("Этот пользователь не зарегистрирован", "Alert");
                        login.ShowDialog();
                    }

                    if (ID == -2)
                    {
                        login = new LoginDel("этот пользователь уже в чате", "Alert");
                        login.ShowDialog();
                    }

                    if (login.condition == -1) Application.Current.Shutdown();

                }
                catch (EndpointNotFoundException)
                {
                    login = new LoginDel("Не удалось подключится к серверу", "Alert");
                    client = new ServiceChatClient(new InstanceContext(this));
                    login.ShowDialog();
                }
                catch (FaultException)
                {
                    login = new LoginDel("Сервер недоступен", "Alert");
                    client = new ServiceChatClient(new InstanceContext(this));
                    login.ShowDialog();
                }
                catch (ProtocolException)
                {
                    //TODO реализовать ProtocolException
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
                    login = new LoginDel("Вы успешно зарегистрированны", "Success");
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
            if (ID != 0 & client.State != CommunicationState.Faulted)
            {
                client.Disconnect(ID);
                client = null;
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
            if (e.Key == Key.Enter)
                if (client != null & client.State != CommunicationState.Faulted)
                {
                    client.SendGeneralMsg(tbMessage.Text, ID);
                    tbMessage.Text = string.Empty;
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
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Файл TXT(*.txt)|*.txt|Файл WORD 2010(*.doc)| *.doc|Файл WORD 2013(*.docx)| *.docx",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            openFileDialog.ShowDialog();

            if (openFileDialog.FileName!="")
            {
                History history = new History(openFileDialog.FileName);

                history.ShowDialog(); 
            }
        }
    }
}