using Chat.Core;
using TechTalk.SpecFlow;

namespace Testing
{
	[Binding]
	public sealed class ChatHostSteps
	{

		private readonly ScenarioContext mScenarioContext;

		public ChatHostSteps(ScenarioContext scenarioContext)
		{
			mScenarioContext = scenarioContext;
		}

		[Given(@"the user is added with the id: (\d+), username: (.*), first name: (.*), last name: (.*), password: (.*)")]
		public static async Task GivenUserIsAddedAsync(int id, string username, string firstName, string lastName, string password)
		{
			await WhenUserIsAddedAsync(username, firstName, lastName, password);
			await ThenUserIsExistsAsync(id, username, firstName, lastName, password);
		}

		[Given(@"the chat is added with the name: (.*), owner id: (\d+) and users:")]
		[When(@"the chat is added with the name: (.*), owner id: (\d+) and users:")]
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

			await DI.DataStore.AddNewChatAsync(chat, usersInChat);
		}

		[When(@"the user is added with the username: (.*), first name: (.*), last name: (.*), password: (.*)")]
		public static async Task WhenUserIsAddedAsync(string username, string firstName, string lastName, string password)
		{
			var registerCredentials = new RegisterCredentialsApiModel()
			{
				Username = username,
				FirstName = firstName,
				LastName = lastName,
				Password = password
			};

			await DI.DataStore.AddNewUserAsync(registerCredentials);
		}

		[When(@"the message is added with the chat id: (\d+), user id: (\d+), content: (.*), creation date: (.*)")]
		public static async Task WhenUserChangedProfileDetailsAsync(int chatId, int userId, byte[] content, DateTime creationDate)
		{
			var message = new MessageDataModel()
			{
				ChatId = chatId,
				UserId = userId,
				Content = content,
				CreationDate = creationDate
			};

			await DI.DataStore.AddNewMessageAsync(message);
		}

		[When(@"the user with the id: (\d+) read message with id: (\d+) in the chat with id: (\d+)")]
		public static async Task WhenUserReadMessageAsync(int userId, int messageId, int chatId)
		{
			var message = new MessageDataModel()
			{
				Id = messageId,
				UserId = userId,
				ChatId = chatId,
			};

			await DI.DataStore.UpdateChatMessageStatusAsync(message);
		}

		[When(@"the user with an id: (\d+) changed username to (.*), first name to (.*), last name to (.*)")]
		public static async Task WhenUserChangedProfileDetailsAsync(int id, string username, string firstName, string lastName)
		{
			var userProfile = new UserProfileDetailsApiModel()
			{
				Id = id,
				Username = username,
				FirstName = firstName,
				LastName = lastName
			};

			await DI.DataStore.UpdateUserProfileDetailsAsync(userProfile);
		}

		[Then(@"the user exists with the id: (\d+), username: (.*), first name: (.*), last name: (.*), password: (.*)")]
		public static async Task ThenUserIsExistsAsync(int id, string username, string firstName, string lastName, string password)
		{
			var loginCredentials = new LoginCredentialsApiModel()
			{
				Username = username,
				Password = password
			};

			var user = await DI.DataStore.GetUserProfileDetailsAsync(loginCredentials);

			AssertHelper.AreEqual(id, user.Id, $"received {user.Username} id");
			AssertHelper.AreEqual(username, user.Username, "received user username");
			AssertHelper.AreEqual(firstName, user.FirstName, "received first name");
			AssertHelper.AreEqual(lastName, user.LastName, "received last name");
		}

		[Then(@"the user with the id: (\d+), username: (.*) is a member of chat rooms: (.*)")]
		public static async Task ThenUserIsMemberOfChatRoomsAsync(int id, string username, List<string> chatRooms)
		{
			var userProfile = new UserProfileDetailsApiModel()
			{
				Id = id,
				Username = username
			};

			var chats = await DI.DataStore.GetListOfChatsAsync(userProfile);
			var chatsNames = chats.Select(chat => chat.Name);

			CollectionAssertHelper.AreEqual(chatsNames, chatRooms, "received chats for user");
		}

		[Then(@"the chat with id: (\d+), name: (.*) contains messages: (.*)")]
		public static async Task ThenChatContainsMessagesAsync(int id, string name, List<string> messages)
		{
			var chat = new ChatDataModel()
			{
				Id = id,
				Name = name
			};

			var recivedMessages = await DI.DataStore.GetMessagesForChatAsync(chat);
			var messagesContent = recivedMessages.Select(recivedMessage => StringTransformations.ByteArrayToString(recivedMessage.Content));

			CollectionAssertHelper.AreEqual(messagesContent, messages, "received chats for user");
		}

		[Then(@"the user with the id: (\d+) read message with id: (\d+) in the chat with id: (\d+)")]
		public static async Task ThenUserReadMessageAsync(int userId, int messageId, int chatId)
		{
			var message = new MessageDataModel()
			{
				Id = messageId,
				UserId = userId,
				ChatId = chatId,
			};

			var status = await DI.DataStore.GetMessageStatusAsync(message);
			AssertHelper.IsTrue(status.IsRead, "received message is read status");
		}
	}
}