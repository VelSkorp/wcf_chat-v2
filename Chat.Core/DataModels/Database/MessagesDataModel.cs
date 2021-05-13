using System;

namespace Chat.Core
{
	/// <summary>
	/// The data model for the Messages table
	/// </summary>
	public class MessagesDataModel
	{
		/// <summary>
		/// The unique message id
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// The unique chat id
		/// </summary>
		public int ChatID { get; set; }

		/// <summary>
		/// The unique user id
		/// </summary>
		public int UserID { get; set; }

		/// <summary>
		/// The message content
		/// </summary>
		public object Content { get; set; }

		/// <summary>
		/// Date the message was created
		/// </summary>
		public DateTime DateCreate { get; set; }
	}
}