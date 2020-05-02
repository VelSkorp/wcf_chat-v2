using System.Windows;
using System.IO;

namespace ChatClient
{
    public partial class History : Window
    {
        public History(string filePath)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);

            while (!streamReader.EndOfStream)
                lbChatHistory.Items.Add(streamReader.ReadLine());

            streamReader.Close();
        }
    }
}