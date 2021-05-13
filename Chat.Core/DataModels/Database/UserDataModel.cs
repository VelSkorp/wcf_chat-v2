namespace Chat.Core
{
	/// <summary>
	/// The data model for the User table
	/// </summary>
	public class UserDataModel
	{
		/// <summary>
		/// The unique id
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// The users username
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// The users first name
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// The users last name
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// The users password
		/// </summary>
		public string Password { get; set; }
	}
}