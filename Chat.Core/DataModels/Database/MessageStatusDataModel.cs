namespace Chat.Core
{
	/// <summary>
	/// The data model for the Message_status table
	/// </summary>
	public class MessageStatusDataModel
	{
		/// <summary>
		/// The unique message id
		/// </summary>
		public int MessageID { get; set; }

		/// <summary>
		/// The unique user id
		/// </summary>
		public int UserID { get; set; }

		/// <summary>
		/// The flag the user has read the message
		/// </summary>
		public bool IsRead { get; set; }
	}
}