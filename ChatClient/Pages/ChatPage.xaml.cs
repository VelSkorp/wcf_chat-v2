using ChatClient.Core;
using System.Windows.Media.Animation;

namespace ChatClient
{
	/// <summary>
	/// Interaction logic for ChatPage.xaml
	/// </summary>
	public partial class ChatPage : BasePage<ChatMessageListViewModel>
	{
		#region Constructors

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

		#endregion

		#region Override methods

		/// <summary>
		/// Fired when the view model changes
		/// </summary>
		protected override void OnViewModelChanged()
		{
			// Make sure UI exists first
			if (ChatMessageList == null)
				return;

			// Fade in chat message list
			var storyboard = new Storyboard();
			storyboard.AddFadeIn(1);
			storyboard.Begin(ChatMessageList);
		}

		#endregion
	}
}