using Chat.Core;
using Dna;

namespace ChatHostWPF
{
	/// <summary>
	/// A shorthand access class to get DI services with nice clean short code
	/// </summary>
	public static class DI
	{
		/// <summary>
		/// A shortcut to access the <see cref="ApplicationViewModel"/>
		/// </summary>
		public static HostApplicationViewModel ApplicationViewModel => Framework.Service<HostApplicationViewModel>();
	}
}