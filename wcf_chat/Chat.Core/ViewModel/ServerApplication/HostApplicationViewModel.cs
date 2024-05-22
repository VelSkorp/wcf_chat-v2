namespace Chat.Core
{
	/// <summary>
	/// The application state as a view model
	/// </summary>
	public class HostApplicationViewModel : BaseViewModel
	{
		/// <summary>
		/// The current page of the application
		/// </summary>
		public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Server;
	}
}