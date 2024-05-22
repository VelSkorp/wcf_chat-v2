using Chat.Core;
using System.Windows.Controls;

namespace ChatClient
{
	/// <summary>
	/// Interaction logic for SettingsControl.xaml
	/// </summary>
	public partial class SettingsControl : UserControl
	{
		public SettingsControl()
		{
			InitializeComponent();

			// Set data	context to settings view model
			DataContext = DI.ChatSettingsViewModel;
		}
	}
}