using CoreWCF;
using System;
using System.Threading.Tasks;

namespace Chat.Core
{
	[ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
	public class ServiceChat : IServiceChat
	{
		#region Public Properties

		/// <summary>
		/// A singleton instance of this class
		/// </summary>
		public static ServiceChat Instance { get; private set; } = new ServiceChat(); 
		
		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ServiceChat()
		{

		}

		#endregion

		#region Interface Implementation

		/// <summary>
		/// Logs in a user
		/// </summary>
		/// <param name="loginCredentials">The login details</param>
		/// <returns>Returns the result of the login request</returns>
		public async Task<ApiResponse<UserProfileDetailsApiModel>> ConnectAsync(LoginCredentialsApiModel loginCredentials)
		{
			// Ensure the data store 
			await CoreDI.DataStore.EnsureDataStoreAsync();

			// The message when we fail to login
			var invalidErrorMessage = "Invalid username or password";

			// The error response for a failed login
			var errorResponse = new ApiResponse<UserProfileDetailsApiModel>
			{
				// Set error message
				ErrorMessage = invalidErrorMessage
			};

			// Make sure we have a user name
			if (loginCredentials?.Username == null || string.IsNullOrWhiteSpace(loginCredentials.Username))
			{
				// Return error message to user
				return errorResponse;
			}

			// Get user profile details
			UserProfileDetailsApiModel userProfileDetails = await CoreDI.DataStore.GetUserProfileDetailsAsync(loginCredentials);

			// If we failed to find a user...
			if (userProfileDetails == null)
			{
				// Return error message to user
				return errorResponse;
			}

			// If we get here, we are valid and the user passed the correct login details

			return new ApiResponse<UserProfileDetailsApiModel>
			{
				// Pass back the user details
				Response = userProfileDetails
			};
		}

		/// <summary>
		/// Tries to register for a new account on the server
		/// </summary>
		/// <param name="registerCredentials">The registration details</param>
		/// <returns>Returns the result of the register request</returns>
		public async Task<ApiResponse<RegisterResultApiModel>> RegisterAsync(RegisterCredentialsApiModel registerCredentials)
		{
			// Ensure the data store 
			await CoreDI.DataStore.EnsureDataStoreAsync();

			// The message when we fail to login
			var invalidErrorMessage = "Please provide all required details to register for an account";

			// The error response for a failed login
			var errorResponse = new ApiResponse<RegisterResultApiModel>
			{
				// Set error message
				ErrorMessage = invalidErrorMessage
			};

			// If we have no credentials...
			if (registerCredentials == null)
			{
				// Return failed response
				return errorResponse;
			}

			// Make sure we have a user name
			if (string.IsNullOrWhiteSpace(registerCredentials.Username) ||
				string.IsNullOrWhiteSpace(registerCredentials.FirstName) ||
				string.IsNullOrWhiteSpace(registerCredentials.Password))
			{
				// Return error message to user
				return errorResponse;
			}

			// Add new user profile details
			RegisterResultApiModel userProfileDetails = await CoreDI.DataStore.AddNewUserProfileDetailsAsync(registerCredentials);

			// If we failed to add a user...
			if (userProfileDetails == null)
			{
				// Return error message to user
				return new ApiResponse<RegisterResultApiModel>
				{
					// Set error message
					ErrorMessage = "This user is already exists in the database"
				};
			}

			// If we get here, we are already register a new user

			return new ApiResponse<RegisterResultApiModel>
			{
				// Pass back the user details
				Response = userProfileDetails
			};
		}

		public void SendMessage(string message, int id)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}