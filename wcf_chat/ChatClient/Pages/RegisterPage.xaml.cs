using Chat.Core;
using WPF.Core;
using System.Security;

namespace ChatClient
{
	/// <summary>
	/// Interaction logic for RegisterPage.xaml
	/// </summary>
	public partial class RegisterPage : BasePage<RegisterViewModel>, IHavePassword
	{
		/// <summary>
		/// The secure password for this login page
		/// </summary>
		public SecureString Password => PasswordText.SecurePassword;

		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public RegisterPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Constructor with specific view model
		/// </summary>
		/// <param name="specificViewModel">The specific view model to use for this page</param>
		public RegisterPage(RegisterViewModel specificViewModel = null)
			: base(specificViewModel)
		{
			InitializeComponent();
		}

		#endregion
	}
}