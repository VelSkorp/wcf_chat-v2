namespace Chat.Core
{
	/// <summary>
	/// The data model for the Party table
	/// </summary>
	public class PartyDataModel
	{
		/// <summary>
		/// The unique chat id
		/// </summary>
		public int ChatID { get; set; }

		/// <summary>
		/// The unique user id
		/// </summary>
		public int UserID { get; set; }
	}
}