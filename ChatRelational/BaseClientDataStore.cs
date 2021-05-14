using Chat.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRelational
{
	/// <summary>
	/// Stores and retrieves information about the client application 
	/// such as login credentials, messages, settings and so on
	/// in an SQLite database
	/// </summary>
	public class BaseClientDataStore : IClientDataStore
	{
		#region Protected Members

		/// <summary>
		/// The database context for the client data store
		/// </summary>
		protected ClientDataStoreDbContext mDbContext;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="dbContext">The database to use</param>
		public BaseClientDataStore(ClientDataStoreDbContext dbContext)
		{
			// Set local member
			mDbContext = dbContext;
		}

		#endregion

		#region Interface Implementation

		/// <summary>
		/// Makes sure the client data store is correctly set up
		/// </summary>
		/// <returns>Returns a task that will finish once setup is complete</returns>
		public async Task EnsureDataStoreAsync()
		{
			// Make sure the database exists and is created
			await mDbContext.Database.EnsureCreatedAsync();
		}

		/// <summary>
		/// Gets the stored login credentials for this client
		/// </summary>
		/// <returns>Returns the login credentials if they exist, or null if none exist</returns>
		public Task<UserProfileDetailsApiModel> GetUserProfileDetailsAsync(LoginCredentialsApiModel loginCredentials)
		{
			// Find user data
			UserDataModel userCredentials = mDbContext.Users.First((user) => user.Username == loginCredentials.Username && user.Password == loginCredentials.Password);

			// If user cann't be found
			if (userCredentials == null)
			{
				return null;
			}

			// Pass back the user details
			return Task.FromResult(new UserProfileDetailsApiModel
			{
				ID = userCredentials.ID,
				FirstName = userCredentials.FirstName,
				LastName = userCredentials.LastName,
				Username = userCredentials.Username,
			});
		}

		/// <summary>
		/// Adds new login credentials for this client
		/// </summary>
		/// <returns>Returns the login credentials if they exist, or null if none exist</returns>
		public Task<RegisterResultApiModel> AddNewUserProfileDetailsAsync(RegisterCredentialsApiModel registerCredentials)
		{
			// Find user data
			UserDataModel userCredentials = mDbContext.Users.First((user) => user.Username == registerCredentials.Username && user.Password == registerCredentials.Password);

			// If user cann't be found
			if (userCredentials != null)
			{
				return null;
			}

			// If we get here, we are have not this user

			userCredentials = new UserDataModel()
			{
				ID = mDbContext.Users.Count() + 1,
				FirstName = registerCredentials.FirstName,
				Username = registerCredentials.Username,
				Password = registerCredentials.Password
			};

			// Add new one
			mDbContext.Users.Add(userCredentials);

			// Pass back the user details
			return Task.FromResult(new RegisterResultApiModel
			{
				ID = userCredentials.ID,
				FirstName = userCredentials.FirstName,
				Username = userCredentials.Username,
			});
		}

		/// <summary>
		/// Stores the given login credentials to the backing data store
		/// </summary>
		/// <param name="loginCredentials">The login credentials to save</param>
		/// <returns>Returns a task that will finish once the save is complete</returns>
		public async Task SaveLoginCredentialsAsync(UserProfileDetailsApiModel loginCredentials)
		{
			// Find user data
			UserDataModel userCredentials = mDbContext.Users.First((user) => user.ID == loginCredentials.ID);

			// Clear all entries
			mDbContext.Users.Remove(userCredentials);

			userCredentials.Username = loginCredentials.Username;
			userCredentials.FirstName = loginCredentials.FirstName;
			userCredentials.LastName = loginCredentials.LastName;

			// Add new one
			mDbContext.Users.Add(userCredentials);

			// Save changes
			await mDbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Removes all login credentials stored in the data store
		/// </summary>
		/// <returns></returns>
		public async Task ClearAllLoginCredentialsAsync()
		{
			// Clear all entries
			mDbContext.Users.RemoveRange(mDbContext.Users);

			// Save changes
			await mDbContext.SaveChangesAsync();
		}

		#endregion
	}
}