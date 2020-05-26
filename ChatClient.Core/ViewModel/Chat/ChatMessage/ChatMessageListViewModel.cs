﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ChatClient.Core
{
	/// <summary>
	/// A view model for a chat message thread list
	/// </summary>
	public class ChatMessageListViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The chat thread items for the list
		/// </summary>
		public ObservableCollection<ChatMessageListItemViewModel> Items { get; set; }

		/// <summary>
		/// True to show the attachment menu, false to hide it
		/// </summary>
		public bool AttachmentMenuVisible { get; set; }

		/// <summary>
		/// True if any popup menus are visible
		/// </summary>
		public bool AnyPopupVisible => AttachmentMenuVisible;

		/// <summary>
		/// The view model for the attachment menu
		/// </summary>
		public ChatAttachmentPopupMenuViewModel AttachmentMenu { get; set; }

		/// <summary>
		/// The text fot the current message being written
		/// </summary>
		public string PendingMessageText { get; set; }

		#endregion

		#region Public Commands

		/// <summary>
		/// The command for when the attached button is clicked
		/// </summary>
		public ICommand AttachmentButtonCommand { get; set; }

		/// <summary>
		/// The command for when the arae outside of any popup is clicked 
		/// </summary>
		public ICommand PopupClickawayCommand { get; set; }

		/// <summary>
		/// The command for when the user clicks the send button
		/// </summary>
		public ICommand SendCommand { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ChatMessageListViewModel()
		{
			// Create commands
			AttachmentButtonCommand = new RelayCommand(AttachmentButton);
			PopupClickawayCommand = new RelayCommand(PopupClickaway);
			SendCommand = new RelayCommand(Send);

			// Make a default menu
			AttachmentMenu = new ChatAttachmentPopupMenuViewModel();
		}

		#endregion

		#region Command Methods

		/// <summary>
		/// When the attachment button is clicked show/hide	the attachment popup
		/// </summary>
		public void AttachmentButton()
		{
			// Toggle menu visibility
			AttachmentMenuVisible ^= true;
		}

		/// <summary>
		/// When the popup cliakaway area is clicked hide any popups
		/// </summary>
		public void PopupClickaway()
		{
			// Hide attachment menu 
			AttachmentMenuVisible = false;
		}

		/// <summary>
		/// When the user clicks the send button, sends the message 
		/// </summary>
		public void Send()
		{
			if (Items == null)
				Items = new ObservableCollection<ChatMessageListItemViewModel>();

			// Fake send a new message
			Items.Add(new ChatMessageListItemViewModel
			{
				Initials = "LM",
				Message = PendingMessageText,
				MessageSentTime = DateTime.UtcNow,
				SentByMe = true,
				SenderName = "Vlad",
				NewItem = true
			});

			// Clear the panding message text
			PendingMessageText = string.Empty;
		}

		#endregion
	}
}