using Chat.Core;
using NUnit.Framework;

namespace Testing
{
	[TestFixture]
	public class RelationalTests
	{
		[SetUp]
		public async Task SetUpAsync()
		{
			await CoreDI.DataStore.EnsureDataStoreAsync();
		}

		[Test]
		public static async Task GetUserProfileDetailsAsync()
		{
			var registerCredentials = new RegisterCredentialsApiModel()
			{
				Username = "test",
				FirstName = "test",
				LastName = "test",
				Password = "test",
			};
			var loginCredentials = new LoginCredentialsApiModel()
			{
				Username = "test",
				Password = "test",
			};

			await CoreDI.DataStore.AddNewUserAsync(registerCredentials);
			var user = await CoreDI.DataStore.GetUserProfileDetailsAsync(loginCredentials);

			Assert.IsNotNull(user);
			Assert.AreEqual(1, user.ID);
			Assert.AreEqual(registerCredentials.Username, user.Username);
			Assert.AreEqual(registerCredentials.FirstName, user.FirstName);
			Assert.AreEqual(registerCredentials.LastName, user.LastName);
		}

		[Test]
		public static async Task AddNewUserAsync()
		{
			var registerCredentials = new RegisterCredentialsApiModel()
			{
				Username = "test",
				FirstName = "test",
				LastName = "test",
				Password = "test",
			};

			var user = await CoreDI.DataStore.AddNewUserAsync(registerCredentials);

			Assert.IsNotNull(user);
			Assert.AreEqual(1, user.ID);
			Assert.AreEqual(registerCredentials.Username, user.Username);
			Assert.AreEqual(registerCredentials.FirstName, user.FirstName);
			Assert.AreEqual(registerCredentials.LastName, user.LastName);
		}

		[Test]
		public static async Task UpdateUserProfileDetailsAsync()
		{
			var registerCredentials = new RegisterCredentialsApiModel()
			{
				Username = "test",
				FirstName = "test",
				LastName = "test",
				Password = "test",
			};
			var userProfile = new UserProfileDetailsApiModel()
			{
				ID = 1,
				Username = "test1",
				FirstName = "test1",
				LastName = "test1",
			};
			var loginCredentials = new LoginCredentialsApiModel()
			{
				Username = "test1",
				Password = "test",
			};

			await CoreDI.DataStore.AddNewUserAsync(registerCredentials);
			await CoreDI.DataStore.UpdateUserProfileDetailsAsync(userProfile);
			var user = await CoreDI.DataStore.GetUserProfileDetailsAsync(loginCredentials);

			Assert.IsNotNull(user);
			Assert.AreEqual(userProfile.ID, user.ID);
			Assert.AreEqual(userProfile.Username, user.Username);
			Assert.AreEqual(userProfile.FirstName, user.FirstName);
			Assert.AreEqual(userProfile.LastName, user.LastName);
		}

		[TearDown]
		public async Task TearDownAsync()
		{
			await CoreDI.DataStore.ClearAllDataAsync();
		}
	}
}