namespace Chat.Core
{
	/// <summary>
	/// The result of a login request or get user profile details request via API
	/// </summary>
	public class UserProfileDetailsApiModel
	{
		#region Public Properties

		/// <summary>
		/// The unique users id
		/// </summary>
		public int ID { get; set; }

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

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public UserProfileDetailsApiModel()
		{

		}

		#endregion

		#region Public Helper Methods

		/// <summary>
		/// Creates a new <see cref="LoginCredentialsDataModel"/>
		/// from this model
		/// </summary>
		/// <returns></returns>
		public LoginCredentialsDataModel ToLoginCredentialsDataModel()
		{
			return new LoginCredentialsDataModel
			{
				ID = ID,
				FirstName = FirstName,
				LastName = LastName,
				Username = Username,
			};
		}

		#endregion
	}
}