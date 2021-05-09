using ChatClient.Core;
using System.Collections.Generic;

namespace ChatClient
{
	/// <summary>
	/// The design-time data for a <see cref="MenuViewModel"/>
	/// </summary>
	public class MenuDesignModel : MenuViewModel
	{
		#region Singleton

		/// <summary>
		/// A single instance of the design model
		/// </summary>
		public static MenuDesignModel Instance => new MenuDesignModel();

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public MenuDesignModel()
		{
			Items = new List<MenuItemViewModel>(new[]
			{
				new MenuItemViewModel
				{
					Type=MenuItemType.Header,
					Text="Design time header"
				},
				new MenuItemViewModel
				{
					Text="Design time file",
					Icon=IconType.File
				},
				new MenuItemViewModel
				{
					Text="Design time picture",
					Icon=IconType.Picture
				},
			});
		}

		#endregion
	}
}