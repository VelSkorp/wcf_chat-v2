﻿namespace ChatClient.Core
{
	/// <summary>
	/// Details for a message box dialog
	/// </summary>
	public class MessageBoxDialogDesignModel : MessageBoxDialogViewModel
	{
		#region Singleton

		/// <summary>
		/// A single instance of the design model
		/// </summary>
		public static MessageBoxDialogDesignModel Instance => new MessageBoxDialogDesignModel();

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public MessageBoxDialogDesignModel()
		{
			Message = "Desing time messages are fun :)";
			OkText = "OK";
		} 

		#endregion
	}
}