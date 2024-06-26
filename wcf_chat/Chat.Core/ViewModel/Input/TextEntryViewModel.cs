﻿using System.Windows.Input;

namespace Chat.Core
{
	/// <summary>
	/// The view model for a text entry to edit a string value
	/// </summary>
	public class TextEntryViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The label to identify what this value is for
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// The current saved value
		/// </summary>
		public string OriginalText { get; set; }

		/// <summary>
		/// The current non-commit edited text
		/// </summary>
		public string EditedText { get; set; }

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
		public TextEntryViewModel()
		{
			// Create commands
			EditCommnad = new RelayCommand(Edit);
			CancelCommnad = new RelayCommand(Cancel);
			SaveCommnad = new RelayCommand(Save);
		}

		#endregion

		#region Commands Methods

		/// <summary>
		/// Puts the control into edit mode
		/// </summary>
		public void Edit()
		{
			// Set the edited text to the current value
			EditedText = OriginalText;

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
			// TODO: Save content
			OriginalText = EditedText;

			Editing = false;
		}

		#endregion
	}
}