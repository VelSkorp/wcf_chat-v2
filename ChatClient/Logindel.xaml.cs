//using System.Windows;

//namespace ChatClient
//{
//    public partial class LoginDel : Window
//    {
//        public string password { get { return PassBoxPassword.Password; } }
//        public string login { get { return tbLogin.Text; } }
//        public int condition { get; private set; }

//        public LoginDel()
//        {
//            InitializeComponent();
//            WindowStartupLocation = WindowStartupLocation.CenterScreen;
//            condition = -2; //Default
//        }

//        public LoginDel(string messageText, string Caption)
//        {
//            InitializeComponent();
//            WindowStartupLocation = WindowStartupLocation.CenterScreen;
//            MessageBox.Show(messageText, Caption, MessageBoxButton.OKCancel);
//        }

//        private void bSingIn_Click(object sender, RoutedEventArgs e)
//        {
//            LabelLogin.Visibility = Visibility.Hidden;
//            LabelPassword.Visibility = Visibility.Hidden;

//            if (tbLogin.Text != "" & PassBoxPassword.Password != "")
//            {
//                DialogResult = true;
//                condition = 0;
//            }

//            if (tbLogin.Text == "")
//                LabelLogin.Visibility = Visibility.Visible;

//            if (PassBoxPassword.Password == "")
//                LabelPassword.Visibility = Visibility.Visible;
//        }

//        private void bSingUp_Click(object sender, RoutedEventArgs e)
//        {
//            DialogResult = false;
//            condition = 1;
//        }

//        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
//        {
//            condition = -1;
//        }
//    }
//}