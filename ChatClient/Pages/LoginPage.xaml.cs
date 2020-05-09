using ChatClient.Core;
using System.Security;

namespace ChatClient
{
	/// <summary>
	/// Логика взаимодействия для LoginPage.xaml
	/// </summary>
	public partial class LoginPage : BasePage<LoginViewModel>, IHavePassword
	{
		public LoginPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// The secure password for this login page
		/// </summary>
		public SecureString SecurePassword => PasswordText.SecurePassword;
	}
}