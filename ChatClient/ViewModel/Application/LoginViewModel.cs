using Chat.Core;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using static ChatClient.DI;

namespace ChatClient
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
			LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
			RegisterCommand = new RelayCommand(async () => await RegisterAsync());
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
				// TODO: Fake a loginll...
				await Task.Delay(1000);

				//if (Client.Endpoint.Address == null)
				//{
				//	// Display error
				//	await UI.ShowMessage(new MessageBoxDialogViewModel
				//	{
				//		Title = "Load error",
				//		Message = "Server can't be found"
				//	});

				//	return;
				//}

				//// Display error
				//await UI.ShowMessage(new MessageBoxDialogViewModel
				//{
				//	Title = "Endpoint",
				//	Message = Client.Endpoint.Address.Uri.ToString()
				//});

				//var a = await Client.ConnectAsync(
				//	// Create api model
				//	new Chat.Core.Proxy.LoginCredentialsApiModel
				//	{
				//		Username = Username,
				//		Password = (parameter as IHavePassword).SecurePassword.Unsecure()
				//	});

				// Ok successfully loggedin... Now det users data
				// TODO: Ask server for users info

				// TODO: Remove this with real information pulled from our database in future
				ViewModelSettings.FirstName = new TextEntryViewModel { Label = "FirstName", OriginalText = $"Vlad {DateTime.Now}" };
				ViewModelSettings.LastName = new TextEntryViewModel { Label = "LastName", OriginalText = $"Kontsevich {DateTime.Now}" };
				ViewModelSettings.Username = new TextEntryViewModel { Label = "UserName", OriginalText = "Vald" };
				ViewModelSettings.Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "*********" };

				// Go to chat page
				ViewModelApplication.GoToPage(ApplicationPage.Chat);
			});
		}

		/// <summary>
		/// Takes the user to the register page
		/// </summary>
		/// <returns></returns>
		public async Task RegisterAsync()
		{
			// Go to register page?
			DI.ViewModelApplication.GoToPage(ApplicationPage.Register);

			await Task.Delay(1);
		}
	}
}