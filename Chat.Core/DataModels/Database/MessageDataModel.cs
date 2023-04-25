using System;

namespace Chat.Core
{
	/// <summary>
	/// The data model for the Messages table
	/// </summary>
	public class MessageDataModel
	{
		/// <summary>
		/// The unique message id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The unique chat id
		/// </summary>
		public int ChatId { get; set; }

		/// <summary>
		/// The unique user id
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// The message content
		/// </summary>
		public byte[] Content { get; set; }

		/// <summary>
		/// Date the message was created
		/// </summary>
		public DateTime CreationDate { get; set; }
	}
}