﻿using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatClient.Core
{
	/// <summary>
	/// The View Model for a register screen
	/// </summary>
	public class RegisterViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The email of the user
		/// </summary>
		public string Email { get; set; }

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
		/// The command to Register for a new account
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
			RegisterCommand = new RelayParameterizedCommand(async (parameter) => await RegisterAsync(parameter));
			LoginCommand = new RelayCommand(async () => await LoginAsync());
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

				//var email = Email;

				//// IMPORTANT: Never store unsecure password in variable like this
				//var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
			});
		}
		 
		/// <summary>
		/// Takes the user to the Login page
		/// </summary>
		/// <returns></returns>
		public async Task LoginAsync()
		{
			//TODO: Go to Login page?
			IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Login);

			await Task.Delay(1);
		}

	}
}