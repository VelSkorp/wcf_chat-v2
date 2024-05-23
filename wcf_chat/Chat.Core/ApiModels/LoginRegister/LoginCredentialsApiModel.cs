namespace Chat.Core
{
	/// <summary>
	/// The credentials for an API client to log into the server
	/// </summary>
	public class LoginCredentialsApiModel
	{
		/// <summary>
		/// The users username
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// The users password
		/// </summary>
		public string Password { get; set; }
	}
}