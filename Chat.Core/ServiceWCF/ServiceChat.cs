using CoreWCF;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Core
{
	[ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
	public class ServiceChat : IServiceChat
	{
		#region Interface Implementation

		/// <summary>
		/// Logs in a user
		/// </summary>
		/// <param name="loginCredentials">The login details</param>
		/// <returns>Returns the result of the login request</returns>
		public async Task<ApiResponse<UserProfileDetailsApiModel>> ConnectAsync(LoginCredentialsApiModel loginCredentials)
		{
			await CoreDI.DataStore.EnsureDataStoreAsync();

			var response = new ApiResponse<UserProfileDetailsApiModel>
			{
				ErrorMessage = "Invalid username or password"
			};

			if (loginCredentials?.Username == null || string.IsNullOrWhiteSpace(loginCredentials?.Username))
			{
				return response;
			}

			UserProfileDetailsApiModel userProfileDetails = await CoreDI.DataStore.GetUserProfileDetailsAsync(loginCredentials);

			if (userProfileDetails == null)
			{
				return response;
			}

			response.ErrorMessage = null;
			response.Response = userProfileDetails;

			return response;
		}

		/// <summary>
		/// Tries to register for a new account on the server
		/// </summary>
		/// <param name="registerCredentials">The registration details</param>
		/// <returns>Returns the result of the register request</returns>
		public async Task<ApiResponse<UserProfileDetailsApiModel>> RegisterAsync(RegisterCredentialsApiModel registerCredentials)
		{
			await CoreDI.DataStore.EnsureDataStoreAsync();

			var response = new ApiResponse<UserProfileDetailsApiModel>
			{
				ErrorMessage = "Please provide all required details to register for an account"
			};

			if (registerCredentials == null)
			{
				return response;
			}

			if (string.IsNullOrWhiteSpace(registerCredentials.Username) ||
				string.IsNullOrWhiteSpace(registerCredentials.FirstName) ||
				string.IsNullOrWhiteSpace(registerCredentials.LastName) ||
				string.IsNullOrWhiteSpace(registerCredentials.Password))
			{
				return response;
			}

			UserProfileDetailsApiModel userProfileDetails = await CoreDI.DataStore.AddNewUserAsync(registerCredentials);

			if (userProfileDetails == null)
			{
				response.ErrorMessage = "This user is already exists in the database";
				return response;
			}

			response.ErrorMessage = null;
			response.Response = userProfileDetails;

			return response;
		}

		public Task<ApiResponse<List<ChatDataModel>>> GetChatsAsync()
		{
			throw new NotImplementedException();
		}

		public void SendMessage(string message, int chatId)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}