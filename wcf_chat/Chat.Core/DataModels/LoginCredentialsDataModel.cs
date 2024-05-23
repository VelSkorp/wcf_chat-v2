namespace Chat.Core
{
	/// <summary>
	/// The data model for the login credentials of a client
	/// </summary>
	public class LoginCredentialsDataModel
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
	}
}