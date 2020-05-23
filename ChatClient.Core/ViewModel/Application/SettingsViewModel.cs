using System.Windows.Input;

namespace ChatClient.Core
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
		public TextEntryViewModel Password { get; set; }

		/// <summary>
		/// The current users email
		/// </summary>
		public TextEntryViewModel Email { get; set; }

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

			// TODO: Remove this with real information pulled from our database in future
			Name = new TextEntryViewModel { Label = "Name", OriginalText = "Vlad Kontsevich" };
			UserName = new TextEntryViewModel { Label = "UserName", OriginalText = "Vald" };
			Password = new TextEntryViewModel { Label = "Password", OriginalText = "*********" };
			Email = new TextEntryViewModel { Label = "Email", OriginalText = "kontsevichv@mail.ru" };
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

		#endregion
	}
}