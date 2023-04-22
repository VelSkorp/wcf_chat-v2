using NUnit.Framework;

namespace Testing
{
	public static class AssertHelper
	{
		/// <summary>
		/// Verifies that two objects are equal
		/// </summary>
		/// <param name="expected">The value that is expected</param>
		/// <param name="actual">The actual value</param>
		/// <param name="parameter">The parameter name the value of which was expected</param>
		public static void AreEqual<T>(T expected, T actual, string parameter)
		{
			Assert.AreEqual(actual, expected, $"Expected {parameter} to be: {expected}. Actual: {actual}");
		}
	}
}