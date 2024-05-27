using CoreWCF;
using Dna;
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
			var response = new ApiResponse<UserProfileDetailsApiModel>();
			try
			{
				await DI.DataStore.EnsureDataStoreAsync();

				response.Response = await DI.DataStore.GetUserProfileDetailsAsync(loginCredentials);
			}
			catch (InvalidOperationException ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "User is not registered";
			}
			catch (Exception ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "Internal server error";
			}
			return response;
		}

		public async Task<ApiResponse<UserProfileDetailsApiModel>> RegisterAsync(RegisterCredentialsApiModel registerCredentials)
		{
			var response = new ApiResponse<UserProfileDetailsApiModel>();
			try
			{
				await DI.DataStore.EnsureDataStoreAsync();

				response.Response = await DI.DataStore.AddNewUserAsync(registerCredentials);
			}
			catch (InvalidOperationException ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "This user is already exists";
			}
			catch (Exception ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "Internal server error";
			}
			return response;
		}

		public async Task<ApiResponse<List<ChatDataModel>>> GetChatsForUserAsync(UserProfileDetailsApiModel userProfile)
		{
			var response = new ApiResponse<List<ChatDataModel>>();
			try
			{
				response.Response = await DI.DataStore.GetListOfChatsAsync(userProfile);
			}
			catch (Exception ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "Internal server error";
			}
			return response;
		}

		public async Task<ApiResponse<List<MessageDataModel>>> GetMessagesForChatAsync(ChatDataModel chat)
		{
			var response = new ApiResponse<List<MessageDataModel>>();
			try
			{
				response.Response = await DI.DataStore.GetMessagesForChatAsync(chat);
			}
			catch (Exception ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "Internal server error";
			}
			return response;
		}

		public async Task<ApiResponse<MessageStatusDataModel>> GetMessageStatusAsync(MessageDataModel message)
		{
			var response = new ApiResponse<MessageStatusDataModel>();
			try
			{
				response.Response = await DI.DataStore.GetMessageStatusAsync(message);
			}
			catch (InvalidOperationException ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "Internal server error";
			}
			return response;
		}

		public async Task<ApiResponse> CreateChatAsync(ChatDataModel chat, List<UserProfileDetailsApiModel> users)
		{
			var response = new ApiResponse();
			try
			{
				await DI.DataStore.AddNewChatAsync(chat, users);
			}
			catch (InvalidOperationException ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "Internal server error";
			}
			return response;
		}

		public async Task<ApiResponse> SendMessageAsync(MessageDataModel message)
		{
			var response = new ApiResponse();
			try
			{
				await DI.DataStore.AddNewMessageAsync(message);
			}
			catch (InvalidOperationException ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "Internal server error";
			}
			return response;
		}

		public async Task<ApiResponse> UpdateChatMessageStatusAsync(MessageDataModel message)
		{
			var response = new ApiResponse();
			try
			{
				await DI.DataStore.UpdateChatMessageStatusAsync(message);
			}
			catch (InvalidOperationException ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "Status for message can't be find";
			}
			catch (Exception ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "Internal server error";
			}
			return response;
		}

		public async Task<ApiResponse> UpdateUserProfileDetailsAsync(UserProfileDetailsApiModel userProfile)
		{
			var response = new ApiResponse();
			try
			{
				await DI.DataStore.UpdateUserProfileDetailsAsync(userProfile);
			}
			catch (InvalidOperationException ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "User profile can't be find";
			}
			catch (Exception ex)
			{
				FrameworkDI.Logger.LogErrorSource(ex.Message);
				response.ErrorMessage = "Internal server error";
			}
			return response;
		}

		#endregion
	}
}