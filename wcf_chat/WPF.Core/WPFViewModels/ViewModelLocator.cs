using Chat.Core;

namespace WPF.Core
{
	/// <summary>
	/// Locates view models from the IoC for use in binding in Xaml files
	/// </summary>
	public class ViewModelLocator
	{
		#region Public Properties

		/// <summary>
		/// Singleton instance of the locator
		/// </summary>
		public static ViewModelLocator Instance { get; } = new ViewModelLocator();

		/// <summary>
		/// The host application view model
		/// </summary>
		public HostApplicationViewModel HostApplicationViewModel => DI.HostApplicationViewModel;

		/// <summary>
		/// The chat application view model
		/// </summary>
		public ChatApplicationViewModel ChatApplicationViewModel => DI.ChatApplicationViewModel;

		/// <summary>
		/// The chat settings view model
		/// </summary>
		public ChatSettingsViewModel ChatSettingsViewModel => DI.ChatSettingsViewModel;

		#endregion
	}
}