namespace Chat.Core
{
	/// <summary>
	/// The design-time data for a <see cref="ChatSettingsViewModel"/>
	/// </summary>
	public class SettingsDesignModel : ChatSettingsViewModel
	{
		#region Singleton

		/// <summary>
		/// A single instance of the design model
		/// </summary>
		public static SettingsDesignModel Instance => new SettingsDesignModel();

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public SettingsDesignModel()
		{
			FirstName = new TextEntryViewModel { Label = "FirstName", OriginalText = "Vlad" };
			LastName = new TextEntryViewModel { Label = "LastName", OriginalText = "Kontsevich" };
			Username = new TextEntryViewModel { Label = "UserName", OriginalText = "Vlad" };
			Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "*********" };
		} 

		#endregion
	}
}