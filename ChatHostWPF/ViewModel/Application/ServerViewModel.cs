using System.Collections.Generic;
using System.Windows.Input;

namespace ChatHostWPF
{
	/// <summary>
	/// The View Model for a login screen
	/// </summary>
	public class ServerViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The flag that indicates server is started or not
		/// </summary>
		public bool IsStarted { get; set; } = false;

		/// <summary>
		/// The server message log
		/// </summary>
		public List<string> Log { get; set; }

		#endregion

		#region Commands

		/// <summary>
		/// The command to start the server
		/// </summary>
		public ICommand StartTheServerCommand { get; set; }

		/// <summary>
		/// The command to disable the server
		/// </summary>
		public ICommand DisableTheServerCommand { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ServerViewModel()
		{
			// Create commands
			StartTheServerCommand = new RelayCommand(StartTheServer);
			DisableTheServerCommand = new RelayCommand(DisableTheServer);
		}

		#endregion

		#region Commands Methods

		/// <summary>
		/// Start the server
		/// </summary>
		public void StartTheServer()
		{
			IsStarted = true;
		}

		/// <summary>
		/// Start the server
		/// </summary>
		public void DisableTheServer()
		{
			IsStarted = false;
		} 

		#endregion
	}
}