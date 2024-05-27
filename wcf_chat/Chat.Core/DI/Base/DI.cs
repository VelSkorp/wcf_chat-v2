using Dna;

namespace Chat.Core
{
	/// <summary>
	/// The IoC container for our application
	/// </summary>
	public static class DI
	{
		/// <summary>
		/// A shortcut to access the <see cref="HostApplicationViewModel"/>
		/// </summary>
		public static HostApplicationViewModel HostApplicationViewModel => Framework.Service<HostApplicationViewModel>();

		/// <summary>
		/// A shortcut to access the <see cref="Core.ChatApplicationViewModel"/>
		/// </summary>
		public static ChatApplicationViewModel ChatApplicationViewModel => Framework.Service<ChatApplicationViewModel>();

		/// <summary>
		/// A shortcut to access the <see cref="Chat.Core.ChatSettingsViewModel"/>
		/// </summary>
		public static ChatSettingsViewModel ChatSettingsViewModel => Framework.Service<ChatSettingsViewModel>();

		/// <summary>
		/// A shortcut to access the <see cref="IFileManager"/>
		/// </summary>
		public static IFileManager File => Framework.Service<IFileManager>();

		/// <summary>
		/// A shortcut to access the <see cref="ITaskManager"/>
		/// </summary>
		public static ITaskManager Task => Framework.Service<ITaskManager>();

		/// <summary>
		/// A shortcut to access toe <see cref="IDataStore"/> service
		/// </summary>
		public static IDataStore DataStore => Framework.Service<IDataStore>();

		/// <summary>
		/// A shortcut to access the <see cref="IUIManager"/>
		/// </summary>
		public static IUIManager UI => Framework.Service<IUIManager>();

		/// <summary>
		/// A shortcut to access the <see cref="IServiceChat"/> as a chat client
		/// </summary>
		public static IServiceChat Client => Framework.Service<IServiceChat>();
	}
}