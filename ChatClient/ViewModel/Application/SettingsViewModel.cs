using System.Windows.Input;

namespace ChatClient
{
	/// <summary>
	/// The settings state as a view model
	/// </summary>
	public class SettingsViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The current users name
		/// </summary>
		public TextEntryViewModel Name { get; set; }

		/// <summary>
		/// The current users username
		/// </summary>
		public TextEntryViewModel UserName { get; set; }

		/// <summary>
		/// The current users password
		/// </summary>
		public PasswordEntryViewModel Password { get; set; }

		/// <summary>
		/// The current users email
		/// </summary>
		public TextEntryViewModel Email { get; set; }

		/// <summary>
		/// The text for the logout button
		/// </summary>
		public string LogoutButtonText { get; set; }

		#endregion

		#region Public Commands

		/// <summary>
		/// The command to open the settings menu
		/// </summary>
		public ICommand OpenCommand { get; set; }

		/// <summary>
		/// The command to close the settings menu
		/// </summary>
		public ICommand CloseCommand { get; set; }

		/// <summary>
		/// The command to logout of the application
		/// </summary>
		public ICommand LogoutCommand { get; set; }

		/// <summary>
		/// The command to clear the users data from the view model
		/// </summary>
		public ICommand ClearUserDataCommand { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public SettingsViewModel()
		{
			// Create commands
			CloseCommand = new RelayCommand(Close);			
			OpenCommand = new RelayCommand(Open);
			LogoutCommand = new RelayCommand(Logout);
			ClearUserDataCommand = new RelayCommand(ClearUserData);

			// TODO: Remove this with real information pulled from our database in future
			Name = new TextEntryViewModel { Label = "Name", OriginalText = "Vlad Kontsevich" };
			UserName = new TextEntryViewModel { Label = "UserName", OriginalText = "Vald" };
			Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "*********" };
			Email = new TextEntryViewModel { Label = "Email", OriginalText = "kontsevichv@mail.ru" };

			// TODO: Get from localization
			LogoutButtonText = "Logout";
		}

		#endregion

		#region Command Methods

		/// <summary>
		/// Closes the settings menu
		/// </summary>
		private void Close()
		{
			// Close the settings menu
			IoC.Application.SettingsMenuVisible = false;
		}

		/// <summary>
		/// Opens the settings menu
		/// </summary>
		private void Open()
		{
			// Open the settings menu
			IoC.Application.SettingsMenuVisible = true;
		}

		/// <summary>
		/// Logs the user out
		/// </summary>
		private void Logout()
		{
			// TODO: Confirm the user wants to logout

			// TODO: Clear any data/cache

			// Clear all application level view models that contains
			// any informatioon about the current user
			ClearUserData();

			// Go to login page
			IoC.Application.GoToPage(ApplicationPage.Login);
		}

		/// <summary>
		/// Clears any data specific to the current user
		/// </summary>
		public void ClearUserData()
		{
			// Clear all view models containing the user info
			Name = null;
			UserName = null;
			Password = null;
			Email = null;
		}

		#endregion
	}
}