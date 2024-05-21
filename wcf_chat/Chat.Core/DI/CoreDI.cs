using Dna;

namespace Chat.Core
{
	/// <summary>
	/// The IoC container for our application
	/// </summary>
	public static class CoreDI
	{
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
	}
}