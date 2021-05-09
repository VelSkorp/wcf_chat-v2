using System.ServiceModel;

namespace Chat.Core
{
	public class UserSarver
	{
		public int ID { get; set; }
		public string  Name { get; set; }
		public string Password { get; set; }
		public OperationContext OperationContext { get; set; }
	}
}