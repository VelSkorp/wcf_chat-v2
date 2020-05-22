using ChatClient.Core;
using System.Threading.Tasks;

namespace ChatClient
{
	/// <summary>
	/// The application implementation of the <see cref="IUIManager"/>
	/// </summary>
	public class UIManager : IUIManager
	{
		/// <summary>
		/// Disaplys a simple message box to the user
		/// </summary>
		/// <param name="viewModel">The view model</param>
		/// <returns></returns>
		public Task ShowMessage(MessageBoxDialogViewModel viewModel)
		{
			return new DialogMessageBox().ShowDialog(viewModel);
		}
	}
}