using System.Collections.Generic;

namespace ChatClient.Core
{
	/// <summary>
	/// The design-time for a <see cref="ChatListViewModel"/>
	/// </summary>
	public class ChatListDesignModel : ChatListViewModel
	{
		#region Singleton

		/// <summary>
		/// A single instance of the design moddel
		/// </summary>
		public static ChatListDesignModel Instance => new ChatListDesignModel();

		#endregion

		#region Constructor

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ChatListDesignModel()
		{
			Items = new List<ChatListItemViewModel>()
			{
				new ChatListItemViewModel
				{
					Initials = "LM",
					Name = "Luke",
					Message = "This new chat app is aswesome! I bet it will be fast too",
					ProfilePictureRGB = "3099c5",
					NewContentAvalible=true
				},
				new ChatListItemViewModel
				{
					Initials = "LM",
					Name = "Luke",
					Message = "This new chat app is aswesome! I bet it will be fast too",
					ProfilePictureRGB = "FF00FF"
				},
				new ChatListItemViewModel
				{
					Initials = "LM",
					Name = "Luke",
					Message = "This new chat app is aswesome! I bet it will be fast too",
					ProfilePictureRGB = "0F03Fe",
					IsSelected=true
				},
				new ChatListItemViewModel
				{
					Initials = "LM",
					Name = "Luke",
					Message = "This new chat app is aswesome! I bet it will be fast too",
					ProfilePictureRGB = "3099c5"
				},
				new ChatListItemViewModel
				{
					Initials = "LM",
					Name = "Luke",
					Message = "This new chat app is aswesome! I bet it will be fast too",
					ProfilePictureRGB = "FF00FF",
					NewContentAvalible=true
				},
				new ChatListItemViewModel
				{
					Initials = "LM",
					Name = "Luke",
					Message = "This new chat app is aswesome! I bet it will be fast too",
					ProfilePictureRGB = "3099c5"
				},
				new ChatListItemViewModel
				{
					Initials = "LM",
					Name = "Luke",
					Message = "This new chat app is aswesome! I bet it will be fast too",
					ProfilePictureRGB = "FF00FF"
				},
				new ChatListItemViewModel
				{
					Initials = "LM",
					Name = "Luke",
					Message = "This new chat app is aswesome! I bet it will be fast too",
					ProfilePictureRGB = "0F03Fe"
				},
				new ChatListItemViewModel
				{
					Initials = "LM",
					Name = "Luke",
					Message = "This new chat app is aswesome! I bet it will be fast too",
					ProfilePictureRGB = "3099c5"
				},
				new ChatListItemViewModel
				{
					Initials = "LM",
					Name = "Luke",
					Message = "This new chat app is aswesome! I bet it will be fast too",
					ProfilePictureRGB = "FF00FF"
				},
			};
		}

		#endregion
	}
}