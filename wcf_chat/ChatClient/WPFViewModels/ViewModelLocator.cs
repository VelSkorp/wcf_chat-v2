using Chat.Core;

namespace ChatClient
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
		/// The application view model
		/// </summary>
		public ChatApplicationViewModel ApplicationViewModel => DI.ChatApplicationViewModel;

		/// <summary>
		/// The settings view model
		/// </summary>
		public ChatSettingsViewModel SettingsViewModel => DI.ChatSettingsViewModel;

		#endregion
	}
}