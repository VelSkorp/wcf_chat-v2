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
			Assert.That(actual, Is.EqualTo(expected), $"Expected {parameter} to be: {expected}. Actual: {actual}");
		}

		/// <summary>
		/// Asserts that a condition is true
		/// </summary>
		/// <param name="condition">The evaluated condition</param>
		/// <param name="parameter">The parameter name the value of which was expected</param>
		public static void IsTrue(bool condition, string parameter)
		{
			Assert.That(condition, Is.True, $"Expected {parameter} to be: true");
		}
	}
}