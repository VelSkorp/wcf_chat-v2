using ChatClient.Core;
using System.Windows;
using System.Windows.Input;

namespace ChatClient
{
	/// <summary>
	/// View Model for the custom falt window
	/// </summary>
	class WindowViewModel : BaseViewModel
	{
		#region Private Member

		/// <summary>
		/// The window this view model controls
		/// </summary>
		private static Window mWindow;

		/// <summary>
		/// The margin around the window to allow for a drop shadow
		/// </summary>
		private int mOuterMarginSize = 10;

		/// <summary>
		/// The radius of the adges if the window
		/// </summary>
		private int mWindowRadius = 10;

		/// <summary>
		/// The last known dock position
		/// </summary>
		private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;

		#endregion

		#region Public Properties

		/// <summary>
		/// The smalest wigth the window can go to
		/// </summary>
		public double WindowMinimumWidth { get; set; } = 800;

		/// <summary>
		/// The smalest height the window can go to
		/// </summary>
		public double WindowMinimumHeight { get; set; } = 500;

		/// <summary>
		/// True if the window should be borderless because it is docked or maximized
		/// </summary>
		public bool Borderless => mWindow.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked;

		/// <summary>
		/// The size of the resize border around window
		/// </summary>
		public int ResizeBorder => Borderless ? 0 : 6;

		/// <summary>
		/// The size of the resize border around the window, taking into account the outer margin
		/// </summary>
		public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);

		/// <summary>
		/// The padding of the inner content of the main window
		/// </summary>
		public Thickness InnerContentPadding { get; set; } = new Thickness(0);

		/// <summary>       
		/// The margin around the window to allow for a drop shadow
		/// </summary>
		public int OuterMarginSize
		{
			// If it is maximized or docked, no border
			get => Borderless ? 0 : mOuterMarginSize;
			set => mOuterMarginSize = value;
		}

		/// <summary>       
		/// The margin around the window to allow for a drop shadow
		/// </summary>
		public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);

		/// <summary>       
		/// The size of the resize border around window
		/// </summary>
		public int WindowRadius
		{
			// If it is maximized or docked, no border
			get => Borderless ? 0 : mWindowRadius;
			set => mWindowRadius = value;
		}

		/// <summary>       
		/// The size of the resize border around window
		/// </summary>
		public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

		/// <summary>
		/// The height of the title bar / caption if the window
		/// </summary>
		public int TitleHeight { get; set; } = 42;

		/// <summary>
		/// The height of the title bar / caption if the window
		/// </summary>
		public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);

		#endregion

		#region Commands

		/// <summary>
		/// The command to minimize the window
		/// </summary>
		public ICommand MinimizeCommand { get; set; }

		/// <summary>
		/// The command to Maximize the window
		/// </summary>
		public ICommand MaximizeCommand { get; set; }

		/// <summary>
		/// The command to Close the window
		/// </summary>
		public ICommand CloseCommand { get; set; }

		/// <summary>
		/// The command to show the system menu of the window
		/// </summary>
		public ICommand MenuCommand { get; set; }

		#endregion

		#region Constuctor

		/// <summary>
		/// Default Constuctor
		/// </summary>
		public WindowViewModel(Window window)
		{
			mWindow = window;

			// Listen out for the window resizing
			window.StateChanged += (sender, e) =>
			{
				// Fire off events for all properties that are affected by a resize
				WindowResized();
			};

			// Create commnds
			MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
			MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
			CloseCommand = new RelayCommand(() => mWindow.Close());
			MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

			// Fix window resize issue
			var resizer = new WindowResizer(mWindow);

			// Listen out for dock changes
			resizer.WindowDockChanged += (dock) =>
			{
				// Store last position
				mDockPosition = dock;

				// Fire off resize events
				WindowResized();
			};
		}

		#endregion

		#region Private Helpers

		/// <summary>
		/// Gets the current mouse position in the screen
		/// </summary>
		/// <returns></returns>
		private static Point GetMousePosition()
		{
			// Position of the mouse relative to th window
			var position = Mouse.GetPosition(mWindow);

			// Add the window position so its a "ToScreen"
			return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
		}

		/// <summary>
		/// If the window resizes to a special position (docked or maximized)
		/// this will update all required property change events to set the borders and radius values
		/// </summary>
		private void WindowResized()
		{
			// Fire off events for all properties that are affected by a resize
			OnPropertyChanged(nameof(Borderless));
			OnPropertyChanged(nameof(ResizeBorderThickness));
			OnPropertyChanged(nameof(OuterMarginSize));
			OnPropertyChanged(nameof(OuterMarginSizeThickness));
			OnPropertyChanged(nameof(WindowRadius));
			OnPropertyChanged(nameof(WindowCornerRadius));
		}

		#endregion
	}
}