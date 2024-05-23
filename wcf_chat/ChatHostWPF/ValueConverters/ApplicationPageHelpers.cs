using Chat.Core;
using WPF.Core;
using System.Diagnostics;

namespace ChatHostWPF
{
	/// <summary>
	/// Converts the <see cref="ApplicationPage"/> to an actual view/page
	/// </summary>
	public static class ApplicationPageHelpers
	{
		/// <summary>
		/// Takes a <see cref="ApplicationPage"/> and a view model, if any, and creates the dessired page
		/// </summary>
		/// <param name="page"></param>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		public static BasePage ToBasePage(this ApplicationPage page, object viewModel = null)
		{
			// Find the appropriate page
			switch (page)
			{
				case ApplicationPage.Server:
					return new ServerPage(viewModel as ServerViewModel);

				default:
					Debugger.Break();
					return null;
			}
		}

		/// <summary>
		/// Converts a <see cref="BasePage"/> to the specific <see cref="ApplicationPage"/> that is for that type of page
		/// </summary>
		/// <param name="page"></param>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		public static ApplicationPage ToApplicationPage(this BasePage page)
		{
			// Find application page that matches the base page
			if (page is ServerPage)
			{
				return ApplicationPage.Server; 
			}

			// Alert developer of issue
			Debugger.Break();
			return default;
		}
	}
}