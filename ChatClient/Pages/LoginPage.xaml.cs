using System.Security;

namespace ChatClient
{
	/// <summary>
	/// Interaction logic for LoginPage.xaml
	/// </summary>
	public partial class LoginPage : BasePage<LoginViewModel>, IHavePassword
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public LoginPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Constructor with specific view model
		/// </summary>
		/// <param name="specificViewModel">The specific view model to use for this page</param>
		public LoginPage(LoginViewModel specificViewModel = null) : base(specificViewModel)
		{
			InitializeComponent();
		}

		#endregion

		/// <summary>
		/// The secure password for this login page
		/// </summary>
		public SecureString SecurePassword => PasswordText.SecurePassword;
	}
}