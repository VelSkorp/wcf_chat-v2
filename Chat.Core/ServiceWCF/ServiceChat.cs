using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Chat.Core
{
	[ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
	public class ServiceChat : IServiceChat
	{
		private static ServiceChat Instance;

		protected ServiceChat() { }

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

		public void SendMessage(string message, int id)
		{
			throw new NotImplementedException();
		}

		public bool Register(string name, string password)
		{
			throw new NotImplementedException();
		}

		public static ServiceChat GetInstance()
		{
			if (Instance == null) Instance = new ServiceChat();
			return Instance;
		}
	}
}