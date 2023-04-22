using Chat.Core;
using TechTalk.SpecFlow;

namespace Testing
{
	[Binding]
	public sealed class RelationalDatabaseSteps
	{
		[Given("the user is added with the username: (.*), first name: (.*), last name: (.*), password: (.*)")]
		public static async Task GivenUserIsAddedAsync(string username, string firstName, string lastName, string password)
		{
			await WhenUserIsAddedAsync(username, firstName, lastName, password);
			await ThenUserIsExistsAsync(1, username, firstName, lastName, password);
		}

		[When("the user is added with the username: (.*), first name: (.*), last name: (.*), password: (.*)")]
		public static async Task WhenUserIsAddedAsync(string username, string firstName, string lastName, string password)
		{
			var registerCredentials = new RegisterCredentialsApiModel()
			{
				Username = username,
				FirstName = firstName,
				LastName = lastName,
				Password = password,
			};

			await CoreDI.DataStore.AddNewUserAsync(registerCredentials);
		}

		[When("the chat is added with the name: (.*), owner id: (.*) and users:")]
		public static async Task WhenChatIsAddedAsync(string name, int ownerId, Table users)
		{
			var chat = new ChatDataModel()
			{
				Name = name,
				OwnerId = ownerId
			};

			var usersInChat = new List<UserProfileDetailsApiModel>();

			foreach (var row in users.Rows)
			{
				usersInChat.Add(new UserProfileDetailsApiModel()
				{
					Id = int.Parse(row[0]),
					FirstName = row[1],
					LastName = row[2],
					Username = row[3]
				});
			}

			await CoreDI.DataStore.AddNewChatAsync(chat, usersInChat);
		}

		[When("the user with an id: (.*) changed username to (.*), first name to (.*), last name to (.*)")]
		public static async Task WhenUserChangedProfileDetailsAsync(int id, string username, string firstName, string lastName)
		{
			var userProfile = new UserProfileDetailsApiModel()
			{
				Id = id,
				Username = username,
				FirstName = firstName,
				LastName = lastName,
			};

			await CoreDI.DataStore.UpdateUserProfileDetailsAsync(userProfile);
		}

		[Then("the user exists with the id: (.*), username: (.*), first name: (.*), last name: (.*), password: (.*)")]
		public static async Task ThenUserIsExistsAsync(int id, string username, string firstName, string lastName, string password)
		{
			var loginCredentials = new LoginCredentialsApiModel()
			{
				Username = username,
				Password = password,
			};

			var user = await CoreDI.DataStore.GetUserProfileDetailsAsync(loginCredentials);

			AssertHelper.AreEqual(id, user.Id, "received user id");
			AssertHelper.AreEqual(username, user.Username, "received user username");
			AssertHelper.AreEqual(firstName, user.FirstName, "received first name");
			AssertHelper.AreEqual(lastName, user.LastName, "received last name");
		}

		[Then("the user with the id: (.*), username: (.*) is a member of chat rooms: (.*)")]
		public static async Task ThenUserIsMemberOfChatRoomsAsync(int id, string username, List<string> chatRooms)
		{
			var userProfile = new UserProfileDetailsApiModel()
			{
				Id = id,
				Username = username,
			};

			var chats = await CoreDI.DataStore.GetListOfChatsAsync(userProfile);
			var chatsNames = chats.Select(chat => chat.Name);

			CollectionAssertHelper.AreEqual(chatsNames, chatRooms, "received chats for user");
		}
	}
}