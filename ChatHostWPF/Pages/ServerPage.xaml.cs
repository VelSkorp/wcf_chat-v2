using ChatHost.Core;

namespace ChatHostWPF
{
	/// <summary>
	/// Interaction logic for ServerPage.xaml
	/// </summary>
	public partial class ServerPage : BasePage<ServerViewModel>
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public ServerPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Constructor with specific view model
		/// </summary>
		/// <param name="specificViewModel">The specific view model to use for this page</param>
		public ServerPage(ServerViewModel specificViewModel = null) : base(specificViewModel)
		{
			InitializeComponent();
		}

		#endregion
	}
}
