using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chat.Core
{
	/// <summary>
	/// A basic command that runs an Action
	/// </summary>
	public class RelayParameterizedAsyncCommand : ICommand
	{
		#region Private Members

		/// <summary>
		/// The action to run
		/// </summary>
		private Func<object, Task> mFunc;

		#endregion

		#region Public Events

		/// <summary>
		/// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
		/// </summary>
		public event EventHandler CanExecuteChanged = (sender, e) => { };

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public RelayParameterizedAsyncCommand(Func<object, Task> func)
		{
			mFunc = func;
		}

		#endregion

		#region Command Methods

		/// <summary>
		/// A relay command can always execute
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		public bool CanExecute(object parameter)
		{
			return true;
		}

		/// <summary>
		/// Executes the commands Action
		/// </summary>
		/// <param name="parameter"></param>
		public async void Execute(object parameter)
		{
			await Task.Run(async () => await mFunc(parameter));
		}

		#endregion
	}
}