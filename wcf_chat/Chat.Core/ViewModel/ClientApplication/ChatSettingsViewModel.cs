using System.Windows.Input;

namespace Chat.Core
{
	/// <summary>
	/// The settings state as a view model
	/// </summary>
	public class ChatSettingsViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The current users first name
		/// </summary>
		public TextEntryViewModel FirstName { get; set; }

		/// <summary>
		/// The current users last name
		/// </summary>
		public TextEntryViewModel LastName { get; set; }

		/// <summary>
		/// The current users username
		/// </summary>
		public TextEntryViewModel Username { get; set; }

		/// <summary>
		/// The current users password
		/// </summary>
		public PasswordEntryViewModel Password { get; set; }

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
		public ChatSettingsViewModel()
		{
			// Create commands
			CloseCommand = new RelayCommand(Close);			
			OpenCommand = new RelayCommand(Open);
			LogoutCommand = new RelayCommand(Logout);
			ClearUserDataCommand = new RelayCommand(ClearUserData);

			// TODO: Remove this with real information pulled from our database in future
			FirstName = new TextEntryViewModel { Label = "FirstName", OriginalText = "Vlad" };
			LastName = new TextEntryViewModel { Label = "LastName", OriginalText = "Kontsevich" };
			Username = new TextEntryViewModel { Label = "UserName", OriginalText = "Vlad" };
			Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "*********" };

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
			DI.ChatApplicationViewModel.SettingsMenuVisible = false;
		}

		/// <summary>
		/// Opens the settings menu
		/// </summary>
		private void Open()
		{
			// Open the settings menu
			DI.ChatApplicationViewModel.SettingsMenuVisible = true;
		}

		/// <summary>
		/// Logs the user out
		/// </summary>
		private void Logout()
		{
			// TODO: Confirm the user wants to logout

			// TODO: Clear any data/cache

			// Clear all application level view models that contains
			// any information about the current user
			ClearUserData();

			// Go to login page
			DI.ChatApplicationViewModel.GoToPage(ApplicationPage.Login);
		}

		/// <summary>
		/// Clears any data specific to the current user
		/// </summary>
		public void ClearUserData()
		{
			// Clear all view models containing the user info
			FirstName = null;
			LastName = null;
			Username = null;
			Password = null;
		}

		#endregion
	}
}