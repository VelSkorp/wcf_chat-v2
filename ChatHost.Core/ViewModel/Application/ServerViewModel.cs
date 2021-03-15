using System.Collections.Generic;
using System.Windows.Input;

namespace ChatHost.Core
{
	/// <summary>
	/// The View Model for a login screen
	/// </summary>
	public class ServerViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// A list of users
		/// </summary>
		public List<string> UsersList { get; set; }

		/// <summary>
		/// The user selected in the ListBox
		/// </summary>
		public string SelectedUser { get; set; }

		/// <summary>
		/// The server message log
		/// </summary>
		public List<string> Log { get; set; }

		#endregion

		#region Commands

		/// <summary>
		/// The command to disable user
		/// </summary>
		public ICommand DisableUserCommand { get; set; }

		/// <summary>
		/// The command to start the server
		/// </summary>
		public ICommand StartTheServerCommand { get; set; }

		/// <summary>
		/// The command to refresh user list
		/// </summary>
		public ICommand RefreshUserListCommand { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ServerViewModel()
		{
			// Create commands
			DisableUserCommand = new RelayCommand(DisableUser);
			StartTheServerCommand = new RelayCommand(StartTheServer);
			RefreshUserListCommand = new RelayCommand(RefreshUserList);
		}

		#endregion

		/// <summary>
		/// Disable user
		/// </summary>
		public void DisableUser()
		{
		}

		/// <summary>
		/// Start the server
		/// </summary>
		public void StartTheServer()
		{
		}

		/// <summary>
		/// Refresh user list
		/// </summary>
		public void RefreshUserList()
		{
		}
	}
}