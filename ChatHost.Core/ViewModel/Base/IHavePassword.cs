﻿using System.Security;

namespace ChatHost.Core
{
	/// <summary>
	/// An interface for a class that can provide a secure password
	/// </summary>
	public interface IHavePassword
	{
		/// <summary>
		/// The secure password
		/// </summary>
		SecureString SecurePassword { get; }
	}
}