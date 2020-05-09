using ChatClient.Core;
using System.Security;

namespace ChatClient
{
	/// <summary>
	/// Логика взаимодействия для RegisterPage.xaml
	/// </summary>
	public partial class RegisterPage : BasePage<RegisterViewModel>, IHavePassword
	{
		public RegisterPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// The secure password for this login page
		/// </summary>
		public SecureString SecurePassword => PasswordText.SecurePassword;
	}
}