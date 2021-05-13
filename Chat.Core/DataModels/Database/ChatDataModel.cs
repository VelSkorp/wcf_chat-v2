namespace Chat.Core
{
	/// <summary>
	/// The data model for the Chat table
	/// </summary>
	public class ChatDataModel
	{
		/// <summary>
		/// The unique chat id
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// The chat name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The unique user id who created chat
		/// </summary>
		public int OwnerID { get; set; }
	}
}