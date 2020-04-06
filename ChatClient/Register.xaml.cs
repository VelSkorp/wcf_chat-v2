using System.Text.RegularExpressions;
using System.Windows;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public string password { get { return PassBoxPassword.Password; } }
        public string login { get { return tbLogin.Text; } }

        public Register()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public Register(string errConnect, string Caption)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MessageBox.Show(errConnect, Caption, MessageBoxButton.OKCancel, MessageBoxImage.Error);
        }

        private void bSingUp_Click(object sender, RoutedEventArgs e)
        {
            LabelLogin.Visibility = Visibility.Hidden;

            LabelPassword.Content = "Это поле должно\nбыть заполненно";
            LabelPassword.FontSize = 12;
            LabelPassword.Visibility = Visibility.Hidden;

            LabelPassword_repeat.Content = "Это поле должно\nбыть заполненно";
            LabelPassword_repeat.Visibility = Visibility.Hidden;

            Regex reg = new Regex(@"[а-я]");
            Match match = reg.Match(PassBoxPassword.Password);

            if (tbLogin.Text != "" & PassBoxPassword.Password != "" & PassBoxPassword_Repeat.Password != "" & PassBoxPassword.Password == PassBoxPassword_Repeat.Password & !match.Success)
                DialogResult = true;

            if (match.Success)
            {
                LabelPassword.Content = "Пароль не должен\nсодрежить русских букв";
                LabelPassword.FontSize = 10;
                LabelPassword.Visibility = Visibility.Visible;
            }

            if (tbLogin.Text == "")
                LabelLogin.Visibility = Visibility.Visible;

            if (PassBoxPassword.Password == "")
                LabelPassword.Visibility = Visibility.Visible;

            if (PassBoxPassword_Repeat.Password != PassBoxPassword.Password)
            {
                LabelPassword_repeat.Content = "Пароли должны\nсовпадать";
                LabelPassword_repeat.Visibility = Visibility.Visible;
            }
        }
    }
}