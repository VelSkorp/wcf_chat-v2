using System.Windows.Input;

namespace ChatClient.Core
{
	/// <summary>
	/// The settings state as a view model
	/// </summary>
	public class SettingsViewModel : BaseViewModel
	{
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