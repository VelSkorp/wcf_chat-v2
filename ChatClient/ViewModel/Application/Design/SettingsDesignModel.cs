﻿namespace ChatClient
{
	/// <summary>
	/// The design-time data for a <see cref="SettingsViewModel"/>
	/// </summary>
	public class SettingsDesignModel : SettingsViewModel
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
			Name = new TextEntryViewModel { Label = "Name", OriginalText = "Vlad Kontsevich" };
			UserName = new TextEntryViewModel { Label = "UserName", OriginalText = "Vald" };
			Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "*********" };
			Email = new TextEntryViewModel { Label = "Email", OriginalText = "kontsevichv@mail.ru" };
		} 

		#endregion
	}
}