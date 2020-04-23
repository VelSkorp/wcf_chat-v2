using System;
using System.ServiceModel;
using System.IO;
using wcf_chat;

namespace ChatHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var host = new ServiceHost(ServiceChat.Instance()))
                {
                    do
                    {
                        Console.WriteLine("1-включить сервер");
                        Console.WriteLine("2-выключить сервер");
                        Console.WriteLine("3-выключить сервер");
                        int ServerAdminChose = int.Parse(Console.ReadLine());                        
                        switch (ServerAdminChose)
                        {
                            case 1:
                                host.Open();
                                Console.WriteLine("хост стартовал");
                                break;
                            case 2:
                                host.Close();
                                Console.WriteLine("хост отключен");
                                break;
                            case 3:
                                var service = host.SingletonInstance as ServiceChat;
                                var users = service.Users;
                                foreach (var user in users)
                                    Console.WriteLine(user.Name);
                                break;
                            default:
                                break;
                        }
                    } while (true);
                }
            }
            catch (Exception ex)
            {
                FileInfo LogFile = new FileInfo(@"./Log.txt");
                StreamWriter streamWriter = LogFile.CreateText();
                streamWriter.Write(ex.Message);
                streamWriter.Close();
            }
        }
    }
}