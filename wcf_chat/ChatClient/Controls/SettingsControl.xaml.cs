using ChatClient;
using System.Windows.Controls;

namespace ChatClient
{
	/// <summary>
	/// Логика взаимодействия для SettingsControl.xaml
	/// </summary>
	public partial class SettingsControl : UserControl
	{
		public SettingsControl()
		{
			InitializeComponent();

			// Set data	context to settings view model
			DataContext = DI.ViewModelSettings;
		}
	}
}