namespace Chat.Core
{
	/// <summary>
	/// The application state as a view model
	/// </summary>
	public class HostApplicationViewModel : BaseViewModel
	{
		/// <summary>
		/// The current page of the host application
		/// </summary>
		public ApplicationPage CurrentPage { get; } = ApplicationPage.Server;

		/// <summary>
		/// The view model to use for the current page when the CurrentPage changes
		/// NOTE: This is not a live up-to-date view model of the current page
		///		  it's simply used to set the view model of the current page 
		///		  at the time it changes
		/// </summary>
		public BaseViewModel CurrentPageViewModel { get; private set; }
	}
}