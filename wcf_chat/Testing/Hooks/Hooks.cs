using Chat.Core;
using Chat.Relational;
using Dna;
using TechTalk.SpecFlow;

namespace Testing
{
	[Binding]
	public sealed class Hooks
	{
		[BeforeFeature("@Database")]
		public static void BeforeDatabaseTestRun()
		{
			Framework.Construct<DefaultFrameworkConstruction>()
				.AddFileLogger()
				.AddDataStore()
				.Build();
		}

		[BeforeScenario("@Database")]
		public static async Task BeforeDatabaseScenarioAsync()
		{
			await DI.DataStore.EnsureDataStoreAsync();
			await DI.DataStore.ClearAllDataAsync();
		}

		[BeforeScenario("@ChatHost")]
		public static void BeforeChatHostScenario(ScenarioContext scenarioContext)
		{
			//var application = Application.Launch("./ChatHostWPF.exe");
			//var window = application.GetWindow("ChatHost", InitializeOption.NoCache);
			//scenarioContext.Add("ChatHostWindow", window);
		}

		[AfterScenario("@Database")]
		public static async Task AfterDatabaseScenarioAsync()
		{
			await DI.DataStore.ClearAllDataAsync();
		}
	}
}