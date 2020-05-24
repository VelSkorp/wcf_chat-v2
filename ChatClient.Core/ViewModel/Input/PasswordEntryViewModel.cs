using System.Data.Odbc;
using System.Security;
using System.Windows.Input;

namespace ChatClient.Core
{
	/// <summary>
	/// The view model for a password entry to edit a password
	/// </summary>
	public class PasswordEntryViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The label to identify what this value is for
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// The fake password display string
		/// </summary>
		public string FakePassword { get; set; }

		/// <summary>
		/// The current password hint text
		/// </summary>
		public string CurrentPasswordHintText { get; set; }

		/// <summary>
		/// The new password hint text
		/// </summary>
		public string NewPasswordHintText { get; set; }

		/// <summary>
		/// The confitm password hint text
		/// </summary>
		public string ConfirmPasswordHintText { get; set; }

		/// <summary>
		/// The current saved passwird
		/// </summary>
		public SecureString CurrentPassword { get; set; }

		/// <summary>
		/// The current non-commit edited password
		/// </summary>
		public SecureString NewPassword { get; set; }

		/// <summary>
		/// The current non-commit edited confirmed password
		/// </summary>
		public SecureString ConfirmPassword { get; set; }

		/// <summary>
		/// Indicates if the current text is in edit mode
		/// </summary>
		public bool Editing { get; set; }

		#endregion

		#region Public Commands

		/// <summary>
		/// Puts the control into edit mode
		/// </summary>
		public ICommand EditCommnad { get; set; }

		/// <summary>
		/// Cancels out of edit mode
		/// </summary>
		public ICommand CancelCommnad { get; set; }

		/// <summary>
		/// Commits the edits and saves the value
		/// as well as goes back to non-edit mode
		/// </summary>
		public ICommand SaveCommnad { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public PasswordEntryViewModel()
		{
			// Create commands
			EditCommnad = new RelayCommand(Edit);
			CancelCommnad = new RelayCommand(Cancel);
			SaveCommnad = new RelayCommand(Save);

			// Set default hints
			// TODO: Replace with localozation text
			CurrentPasswordHintText = "Current Password";
			NewPasswordHintText = "New Password";
			ConfirmPasswordHintText = "Confirm Password";

		}

		#endregion

		#region Commands Methods

		/// <summary>
		/// Puts the control into edit mode
		/// </summary>
		public void Edit()
		{
			// Clear all password
			NewPassword = new SecureString();
			ConfirmPassword = new SecureString();


			// Go into edit mode
			Editing = true;
		}

		/// <summary>
		/// Cancels out of edit mode
		/// </summary>
		public void Cancel()
		{
			Editing = false;
		}

		/// <summary>
		/// Commits the content and exits out of edit mode
		/// </summary>
		public void Save()
		{
			// Make sure current password is correct
			// TODO: This will come from the real back-end store of this users password
			//		 or via	asking the server to confirm it
			var storedPassword = "Testing";

			// Confirm current password is a match
			// Note: Typically this isn't done here, it's done on server
			if (storedPassword != CurrentPassword.Unsecure())
			{
				// Let user know
				IoC.UI.ShowMessage(new MessageBoxDialogViewModel
				{
					Title = "Wrond password",
					Message = "The current password is invalid"
				});

				return;
			}

			// Now check that the new and confirm password match
			if (NewPassword.Unsecure() != ConfirmPassword.Unsecure())
			{
				// Let user know
				IoC.UI.ShowMessage(new MessageBoxDialogViewModel
				{
					Title = "Password mismatch",
					Message = "The new and confirm password do not match"
				});

				return;
			}

			// Check we actualy have a password
			if (NewPassword.Unsecure().Length==0)
			{
				// Let user know
				IoC.UI.ShowMessage(new MessageBoxDialogViewModel
				{
					Title = "Password too short",
					Message = "You must enter a password!"
				});

				return;
			}

			// Set the edited password to the current value
			CurrentPassword = new SecureString();
			foreach (var c in NewPassword.Unsecure().ToCharArray())
				CurrentPassword.AppendChar(c);

			Editing = false;
		}

		#endregion
	}
}