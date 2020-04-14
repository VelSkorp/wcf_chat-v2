using System.Windows;
using System.IO;

namespace ChatClient
{
    public partial class History : Window
    {
        public History(string filePath)
        {
            InitializeComponent();

            FileInfo file = new FileInfo(filePath);
            StreamReader stream = file.OpenText();

            while (!stream.EndOfStream)
                lbChatHistory.Items.Add(stream.ReadLine());

            stream.Close();
        }
    }
}