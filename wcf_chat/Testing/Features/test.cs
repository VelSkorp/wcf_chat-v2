using NUnit.Framework;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;

namespace Testing
{
	[TestFixture]
	public sealed class Test
	{
		[Test]
		public static void SimpleTest()
		{
			var application = Application.Launch("./ChatHostWPF.exe");
			var window = application.GetWindow("ChatHost", InitializeOption.NoCache);

			var but = window.Get<Button>(SearchCriteria.ByAutomationId(""));
		}
	}
}