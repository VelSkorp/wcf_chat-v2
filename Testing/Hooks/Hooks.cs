using Chat.Core;
using ChatRelational;
using Dna;
using TechTalk.SpecFlow;

namespace Testing
{
	[Binding]
	public sealed class Hooks
	{
		[BeforeFeature]
		public static void BeforeTestRun()
		{
			// Setup the Dna Framework
			Framework.Construct<DefaultFrameworkConstruction>()
				.AddFileLogger()
				.AddDataStore()
				.Build();
		}

		[BeforeScenario]
		public static async Task BeforeScenarioAsync()
		{
			await CoreDI.DataStore.EnsureDataStoreAsync();
		}

		[AfterScenario]
		public static async Task AfterScenarioAsync()
		{
			await CoreDI.DataStore.ClearAllDataAsync();
		}
	}
}