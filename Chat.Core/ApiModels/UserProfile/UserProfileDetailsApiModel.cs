namespace Chat.Core
{
	/// <summary>
	/// The result of a login request or get user profile details request via API
	/// </summary>
	public class UserProfileDetailsApiModel
	{
		/// <summary>
		/// The unique users id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The users first name
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// The users last name
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// The users username
		/// </summary>
		public string Username { get; set; }
	}
}