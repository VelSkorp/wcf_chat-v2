using TechTalk.SpecFlow;

namespace Testing
{
	public static class StringTransformations
	{
		[StepArgumentTransformation]
		public static List<string> TransformToListOfString(string commaSeparatedList)
		{
			return commaSeparatedList.Split(",").ToList();
		}
	}
}