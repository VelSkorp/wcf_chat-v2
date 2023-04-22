namespace Chat.Core
{
	/// <summary>
	/// The data model for the Message_status table
	/// </summary>
	public class MessageStatusDataModel
	{
		/// <summary>
		/// The unique message status id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The unique message id
		/// </summary>
		public int MessageId { get; set; }

		/// <summary>
		/// The unique user id
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// The flag the user has read the message
		/// </summary>
		public bool IsRead { get; set; }
	}
}