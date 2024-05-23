using NUnit.Framework;

namespace Testing
{
	public static class CollectionAssertHelper
	{
		/// <summary>
		/// Verifies that two Enumerable objects are equal
		/// </summary>
		/// <param name="expected">The value that is expected</param>
		/// <param name="actual">The actual value</param>
		/// <param name="parameter">The parameter name the value of which was expected</param>
		public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, string parameter)
		{
			Assert.That(actual, Is.EquivalentTo(expected), $"Expected {parameter} to be: {expected}. Actual: {actual}");
		}
	}
}