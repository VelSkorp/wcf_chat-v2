using CoreWCF;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Core
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class ServiceChat : IServiceChat
	{
		#region Interface Implementation

		public async Task<ApiResponse<UserProfileDetailsApiModel>> ConnectAsync(LoginCredentialsApiModel loginCredentials)
		{
			await CoreDI.DataStore.EnsureDataStoreAsync();

			var response = new ApiResponse<UserProfileDetailsApiModel>();

			if (string.IsNullOrWhiteSpace(loginCredentials?.Username) || string.IsNullOrWhiteSpace(loginCredentials?.Password))
			{
				response.ErrorMessage = "Username or password can't be empty";
				return response;
			}

			UserProfileDetailsApiModel userProfileDetails;
			try
			{
				userProfileDetails = await CoreDI.DataStore.GetUserProfileDetailsAsync(loginCredentials);
			}
			catch (InvalidOperationException)
			{
				response.ErrorMessage = "User is not registered";
				return response;
			}

			response.Response = userProfileDetails;
			return response;
		}

		public async Task<ApiResponse<UserProfileDetailsApiModel>> RegisterAsync(RegisterCredentialsApiModel registerCredentials)
		{
			await CoreDI.DataStore.EnsureDataStoreAsync();

			var response = new ApiResponse<UserProfileDetailsApiModel>();

			if (string.IsNullOrWhiteSpace(registerCredentials?.Username) ||
				string.IsNullOrWhiteSpace(registerCredentials?.FirstName) ||
				string.IsNullOrWhiteSpace(registerCredentials?.LastName) ||
				string.IsNullOrWhiteSpace(registerCredentials?.Password))
			{
				response.ErrorMessage = "User information can't be empty";
				return response;
			}

			UserProfileDetailsApiModel userProfileDetails;
			try
			{
				userProfileDetails = await CoreDI.DataStore.AddNewUserAsync(registerCredentials);
			}
			catch (InvalidOperationException)
			{
				response.ErrorMessage = "This user is already exists";
				return response;
			}

			response.Response = userProfileDetails;
			return response;
		}

		public async Task<ApiResponse<List<ChatDataModel>>> GetChatsForUserAsync(UserProfileDetailsApiModel userProfile)
		{
			return new ApiResponse<List<ChatDataModel>>
			{
				Response = await CoreDI.DataStore.GetListOfChatsAsync(userProfile)
			};
		}

		public async Task<ApiResponse<List<MessageDataModel>>> GetMessagesForChatAsync(ChatDataModel chat)
		{
			return new ApiResponse<List<MessageDataModel>>
			{
				Response = await CoreDI.DataStore.GetMessagesForChatAsync(chat)
			};
		}

		public async Task<ApiResponse<MessageStatusDataModel>> GetMessageStatusAsync(MessageDataModel message)
		{
			var response = new ApiResponse<MessageStatusDataModel>();

			MessageStatusDataModel messageStatus;
			try
			{
				messageStatus = await CoreDI.DataStore.GetMessageStatusAsync(message);
			}
			catch (InvalidOperationException)
			{
				response.ErrorMessage = "Something went wrong with receiving message status";
				return response;
			}

			response.Response = messageStatus;
			return response;
		}

		public async Task<ApiResponse> CreateChatAsync(ChatDataModel chat, List<UserProfileDetailsApiModel> users)
		{
			var response = new ApiResponse();
			try
			{
				await CoreDI.DataStore.AddNewChatAsync(chat, users);
			}
			catch (InvalidOperationException)
			{
				response.ErrorMessage = "Something went wrong with creating a new chat";
			}
			return response;
		}

		public async Task<ApiResponse> SendMessageAsync(MessageDataModel message)
		{
			var response = new ApiResponse();
			try
			{
				await CoreDI.DataStore.AddNewMessageAsync(message);
			}
			catch (InvalidOperationException)
			{
				response.ErrorMessage = "Something went wrong with sending message";
			}
			return response;
		}

		public async Task<ApiResponse> UpdateChatMessageStatusAsync(MessageDataModel message)
		{
			var response = new ApiResponse();
			try
			{
				await CoreDI.DataStore.UpdateChatMessageStatusAsync(message);
			}
			catch (InvalidOperationException)
			{
				response.ErrorMessage = "Status for message can't be find";
			}
			catch (Exception)
			{
				response.ErrorMessage = "Something went wrong with updating message status";
			}
			return response;
		}

		public async Task<ApiResponse> UpdateUserProfileDetailsAsync(UserProfileDetailsApiModel userProfile)
		{
			var response = new ApiResponse();
			try
			{
				await CoreDI.DataStore.UpdateUserProfileDetailsAsync(userProfile);
			}
			catch (InvalidOperationException)
			{
				response.ErrorMessage = "User profile can't be find";
			}
			catch (Exception)
			{
				response.ErrorMessage = "Something went wrong with updating user profile";
			}
			return response;
		}

		#endregion
	}
}