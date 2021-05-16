﻿using System.Threading.Tasks;

namespace Chat.Core
{
	/// <summary>
	/// Stores and retrieves information about the client application 
	/// such as login credentials, messages, settings and so on
	/// </summary>
	public interface IClientDataStore
	{
		/// <summary>
		/// Makes sure the client data store is correctly set up
		/// </summary>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		Task EnsureDataStoreAsync();

		/// <summary>
		/// Gets the stored login credentials for this client
		/// </summary>
		/// <returns>Returns the login credentials if they exist, or null if none exist</returns>
		Task<UserProfileDetailsApiModel> GetUserProfileDetailsAsync(LoginCredentialsApiModel loginCredentials);

		/// <summary>
		/// Adds new login credentials for this client
		/// </summary>
		/// <returns>Returns the login credentials if they added, or null if they already exist</returns>
		Task<RegisterResultApiModel> AddNewUserProfileDetailsAsync(RegisterCredentialsApiModel registerCredentials);

		/// <summary>
		/// Stores the given login credentials to the backing data store
		/// </summary>
		/// <param name="loginCredentials">The login credentials to save</param>
		/// <returns>Returns a task that will finish once the save is complete</returns>
		Task SaveLoginCredentialsAsync(UserProfileDetailsApiModel loginCredentials);

		/// <summary>
		/// Removes all login credentials stored in the data store
		/// </summary>
		/// <returns></returns>
		Task ClearAllLoginCredentialsAsync();
	}
}