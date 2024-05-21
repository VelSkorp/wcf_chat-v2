using Chat.Core;
using Chat.Relational;
using Dna;
using TechTalk.SpecFlow;
using TestStack.White;
using TestStack.White.Factory;

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
			await CoreDI.DataStore.EnsureDataStoreAsync();
			await CoreDI.DataStore.ClearAllDataAsync();
		}

		[BeforeScenario("@ChatHost")]
		public static void BeforeChatHostScenario(ScenarioContext scenarioContext)
		{
			var application = Application.Launch("./ChatHostWPF.exe");
			var window = application.GetWindow("ChatHost", InitializeOption.NoCache);
			scenarioContext.Add("ChatHostWindow", window);
		}

		[AfterScenario("@Database")]
		public static async Task AfterDatabaseScenarioAsync()
		{
			await CoreDI.DataStore.ClearAllDataAsync();
		}
	}
}