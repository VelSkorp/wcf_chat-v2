namespace Chat.Core
{
	/// <summary>
	/// The data model for the Roster table
	/// </summary>
	public class RosterDataModel
	{
		/// <summary>
		/// The unique roster id
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
	}
}