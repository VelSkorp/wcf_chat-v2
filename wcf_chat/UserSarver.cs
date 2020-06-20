using System.ServiceModel;

namespace wcf_chat
{
	public class UserSarver
	{
		public int ID { get; set; }
		public string  Name { get; set; }
		public string Password { get; set; }
		public OperationContext OperationContext { get; set; }
	}
}