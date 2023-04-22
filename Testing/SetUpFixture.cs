using ChatRelational;
using Dna;
using NUnit.Framework;

namespace Testing
{
	[SetUpFixture]
	public class SetUpFixture
	{
		[OneTimeSetUp]
		public void SetUp()
		{
			// Setup the Dna Fraimwork
			Framework.Construct<DefaultFrameworkConstruction>()
				.AddFileLogger()
				.AddDataStore()
				.Build();
		}
	}
}