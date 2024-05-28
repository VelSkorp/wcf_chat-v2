using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chat.Core
{
	/// <summary>
	/// The View Model for a login screen
	/// </summary>
	public class LoginViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The username of the user
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// A flag indicating if the login command is running
		/// </summary>
		public bool LoginIsRunning { get; set; }

		#endregion

		#region Commands

		/// <summary>
		/// The command to login
		/// </summary>
		public ICommand LoginCommand { get; set; }

		/// <summary>
		/// The command to register for a new account
		/// </summary>
		public ICommand RegisterCommand { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public LoginViewModel()
		{
			// Create commands
			LoginCommand = new RelayParameterizedAsyncCommand(LoginAsync);
			RegisterCommand = new RelayAsyncCommand(RegisterAsync);
		}

		#endregion

		/// <summary>
		/// Attempts to log the user in
		/// </summary>
		/// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the users password</param>
		/// <returns></returns>
		public async Task LoginAsync(object parameter)
		{
			await RunCommandAsync(() => LoginIsRunning, async () =>
			{
				await Task.Delay(1000);

				var loginCredentials = new LoginCredentialsApiModel
				{
					Username = Username,
					Password = (parameter as IHavePassword).SecurePassword.Unsecure()
				};

				var response = await DI.Client.ConnectAsync(loginCredentials);

				if (response.IsFailed)
				{
					await DI.UI.ShowMessage(new MessageBoxDialogViewModel
					{
						Title = "Wrong Credentials",
						Message = "The current password or username is invalid"
					});

					return;
				}

				DI.ChatSettingsViewModel.Username = new TextEntryViewModel { Label = "Username", OriginalText = Username };

				// Go to chat page
				DI.ChatApplicationViewModel.GoToPage(ApplicationPage.Chat);
			});
		}

		/// <summary>
		/// Takes the user to the register page
		/// </summary>
		/// <returns></returns>
		public async Task RegisterAsync()
		{
			// Go to register page
			DI.ChatApplicationViewModel.GoToPage(ApplicationPage.Register);

			await Task.Delay(1);
		}
	}
}