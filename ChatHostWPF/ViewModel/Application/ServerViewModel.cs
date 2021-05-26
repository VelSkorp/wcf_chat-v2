using Chat.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Discovery;
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
		public bool IsStarted { get; private set; } = false;

		/// <summary>
		/// The server message log
		/// </summary>
		public List<string> Log { get; private set; } = new List<string>();

		#endregion

		#region Private Members

		/// <summary>
		/// ServiceHost for hosting ServiceChat service
		/// </summary>
		private ServiceHost mServiceHost;

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
			try
			{
				// Create base adress for ServiceHost using DNS hostname of the local computer
				var baseAdress = new Uri($"net.tcp://{Dns.GetHostName()}");

				// Create a ServiceHost for the ServiceChat type.  
				mServiceHost = new ServiceHost(ServiceChat.Instance, baseAdress);

				// Add ServiceDiscoveryBehavior  
				mServiceHost.Description.Behaviors.Add(new ServiceDiscoveryBehavior());

				// Add a UdpDiscoveryEndpoint  
				mServiceHost.AddServiceEndpoint(new UdpDiscoveryEndpoint());

				// Open the ServiceHost to create listeners
				// and start listening for messages.  
				mServiceHost.Open();

				IsStarted = true;

				Log.Add($"[Data] Host stated\n");
			}
			catch (Exception ex)
			{
				Log.Add($"[Error] {ex.Message}\n");
			}
			finally
			{
				Log = new List<string>(Log);
			}
		}

		/// <summary>
		/// Start the server
		/// </summary>
		public void DisableTheServer()
		{
			try
			{
				// Close the ServiceHost
				mServiceHost.Close();

				IsStarted = false;

				Log.Add($"[Data] Host stoped\n");
			}
			catch (Exception ex)
			{
				Log.Add($"[Error] {ex.Message}\n");
			}
			finally
			{
				Log = new List<string>(Log);
			}
		}

		#endregion
	}
}