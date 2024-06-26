﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Chat.Core
{
	/// <summary>
	/// A view model for each chat list item in the overview chat list
	/// </summary>
	public class ChatListItemViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The display name of this chat list
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The latest message from this chat
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// The initials to show for the profile picture background
		/// </summary>
		public string Initials { get; set; }

		/// <summary>
		/// The RGB values (in hex) for the background color of the profile picture
		/// For example FF00FF for Red and Blue mixed
		/// </summary>
		public string ProfilePictureRGB { get; set; }

		/// <summary>
		/// True if there are unread messages in this chat 
		/// </summary>
		public bool NewContentAvailable { get; set; }

		/// <summary>
		/// True if this item is currently selected
		/// </summary>
		public bool IsSelected { get; set; }

		#endregion

		#region Public Commands

		/// <summary>
		/// Open the current message thread
		/// </summary>
		public ICommand OpenMessageCommand { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ChatListItemViewModel()
		{
			// Creates commands
			OpenMessageCommand = new RelayCommand(OpenMessage);
		}

		#endregion

		#region Commands Methods

		/// <summary>
		/// 
		/// </summary>
		public void OpenMessage()
		{
			if (Name == "Jesse")
			{
				DI.ChatApplicationViewModel.GoToPage(ApplicationPage.Login, new LoginViewModel
				{
					Username = "Jesse@mail.ru"
				});

				return;
			}

			DI.ChatApplicationViewModel.GoToPage(ApplicationPage.Chat, new ChatMessageListViewModel
			{
				DisplayTitle = "Parnell, Me",

				Items = new ObservableCollection<ChatMessageListItemViewModel>
				{
					new ChatMessageListItemViewModel
					{
						Message = Message,
						Initials = Initials,
						MessageSentTime = DateTime.UtcNow,
						ProfilePictureRGB = "FF00FF",
						SenderName = "Солнышко",
						SentByMe = true,
					},
					new ChatMessageListItemViewModel
					{
						Message = "A recive message",
						Initials = Initials,
						MessageSentTime = DateTime.UtcNow,
						ProfilePictureRGB = "FF0000",
						SenderName = "Vlad",
						SentByMe = false,
					},
					new ChatMessageListItemViewModel
					{
						Message = Message,
						Initials = Initials,
						MessageSentTime = DateTime.UtcNow,
						ProfilePictureRGB = "FF00FF",
						SenderName = "Солнышко",
						SentByMe = true,
					},
					new ChatMessageListItemViewModel
					{
						Message = Message,
						Initials = Initials,
						MessageSentTime = DateTime.UtcNow,
						ProfilePictureRGB = "FF00FF",
						SenderName = "Солнышко",
						SentByMe = true,
					},
					new ChatMessageListItemViewModel
					{
						Message = "A recive message",
						ImageAttachment=new ChatMessageListItemImageAttachmentViewModel
						{
							ThumbnailUrl="http://Velscorp.com"
						},
						Initials = Initials,
						MessageSentTime = DateTime.UtcNow,
						ProfilePictureRGB = "FF0000",
						SenderName = "Vlad",
						SentByMe = false,
					},
				}
			});

		}

		#endregion
	}
}