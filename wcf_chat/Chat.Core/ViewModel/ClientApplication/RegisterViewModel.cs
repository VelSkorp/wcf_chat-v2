﻿using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chat.Core
{
	/// <summary>
	/// The View Model for a register screen
	/// </summary>
	public class RegisterViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The username of the user
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// The first name of the user
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// The last name of the user
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// A flag indicating if the register command is running
		/// </summary>
		public bool RegisterIsRunning { get; set; }

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
		public RegisterViewModel()
		{
			// Create commands
			RegisterCommand = new RelayParameterizedAsyncCommand(RegisterAsync);
			LoginCommand = new RelayAsyncCommand(LoginAsync);
		}

		#endregion

		/// <summary>
		/// Attempts to register a new user
		/// </summary>
		/// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the users password</param>
		/// <returns></returns>
		public async Task RegisterAsync(object parameter)
		{
			await RunCommandAsync(() => RegisterIsRunning, async () =>
			{
				await Task.Delay(5000);

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
			});
		}

		/// <summary>
		/// Takes the user to the login page
		/// </summary>
		/// <returns></returns>
		public async Task LoginAsync()
		{
			// Go to register page
			DI.ChatApplicationViewModel.GoToPage(ApplicationPage.Login);

			await Task.Delay(1);
		}
	}
}