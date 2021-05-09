﻿using Chat.Core;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatClient
{
	/// <summary>
	/// The View Model for a login screen
	/// </summary>
	public class LoginViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The email of the user
		/// </summary>
		public string Email { get; set; }

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

				// Ok successfully loggedin... Now det users data
				// TODO: Ask server for users info

				// TODO: Remove this with real information pulled from our database in future
				DI.ViewModelSettings.Name = new TextEntryViewModel { Label = "Name", OriginalText = $"Vlad Kontsevich {DateTime.Now}" };
				DI.ViewModelSettings.UserName = new TextEntryViewModel { Label = "UserName", OriginalText = "Vald" };
				DI.ViewModelSettings.Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "*********" };
				DI.ViewModelSettings.Email = new TextEntryViewModel { Label = "Email", OriginalText = "kontsevichv@mail.ru" };

				// Go to chat page
				DI.ViewModelApplication.GoToPage(ApplicationPage.Chat);

				//var email = Email;

				//// IMPORTANT: Never store unsecure password in variable like this
				//var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
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