using System.Text;
using TechTalk.SpecFlow;

namespace Testing
{
	[Binding]
	public static class StringTransformations
	{
		[StepArgumentTransformation]
		public static List<string> StringToListOfStrings(string commaSeparatedList)
		{
			return commaSeparatedList.Split(",").ToList();
		}

		[StepArgumentTransformation]
		public static byte[] StringToByteArray(string text)
		{
			return Encoding.UTF8.GetBytes(text);
		}

		[StepArgumentTransformation]
		public static DateTime StringToDateTime(string date)
		{
			return DateTime.Parse(date);
		}

		public static string ByteArrayToString(byte[] text)
		{
			return Encoding.UTF8.GetString(text);
		}
	}
}