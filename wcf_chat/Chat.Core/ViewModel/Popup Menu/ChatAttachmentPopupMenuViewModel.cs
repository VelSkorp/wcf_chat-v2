﻿using System.Collections.Generic;

namespace Chat.Core

{
	/// <summary>
	/// A view model for any popup menus
	/// </summary>
	public class ChatAttachmentPopupMenuViewModel : BasePopupViewModel
	{
		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ChatAttachmentPopupMenuViewModel()
		{
			Content = new MenuViewModel()
			{
				Items = new List<MenuItemViewModel>(new[]
				{
					new MenuItemViewModel { Text = "Attach a file...", Type = MenuItemType.Header },
					new MenuItemViewModel { Text = "From computer", Icon = IconType.File },
					new MenuItemViewModel { Text = "From pictures", Icon = IconType.Picture },
				})
			};
		}

		#endregion
	}
}