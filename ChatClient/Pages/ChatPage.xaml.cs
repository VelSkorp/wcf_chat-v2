using ChatClient.Core;

namespace ChatClient
{
	/// <summary>
	/// Interaction logic for ChatPage.xaml
	/// </summary>
	public partial class ChatPage : BasePage<ChatMessageListViewModel>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ChatPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Constructor with specific view model
		/// </summary>
		/// <param name="specificViewModel">The specific view model to use for this page</param>
		public ChatPage(ChatMessageListViewModel specificViewModel = null) : base(specificViewModel)
		{
			InitializeComponent();
		}
	}
}