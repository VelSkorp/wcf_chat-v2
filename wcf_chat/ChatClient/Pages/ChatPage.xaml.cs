using Chat.Core;
using WPF.Core;
using System;
using System.Windows.Controls;
using System.Windows.Input;
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
		public ChatPage(ChatMessageListViewModel specificViewModel = null)
			: base(specificViewModel)
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
			if (ChatMessageList is null)
			{
				return;
			}

			// Fade in chat message list
			var storyboard = new Storyboard();
			storyboard.AddFadeIn(1, from: true);
			storyboard.Begin(ChatMessageList);

			// Make the message box focused
			MessageText.Focus();
		}

		#endregion

		/// <summary>
		/// Preview the input into the message box and respond as required
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MessageText_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			// Get the text box
			var textBox = sender as TextBox;

			// Check if we have pressed enter
			if (e.Key == Key.Enter)
			{
				// If we have control pressed...
				if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
				{
					// Add a new line at the point where the cursor is
					var index = textBox.CaretIndex;

					// Insert a new line
					textBox.Text = textBox.Text.Insert(index, Environment.NewLine);

					// Shift the caret forwarad to the new line 
					textBox.CaretIndex = index + Environment.NewLine.Length;

					// Mark this key as handled by us
					e.Handled = true;
				}
				else
				{
					// Send the message
					ViewModel.Send();
				}

				// Mark the key as handled
				e.Handled = true;
			}
		}
	}
}