using ChatClient.Core;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatClient
{
	/// <summary>
	/// A base page for all pages to gain base functionality
	/// </summary>
	public class BasePage<VM> : Page
		where VM : BaseViewModel, new()
	{
		#region Private Mamber

		/// <summary>
		/// The View Model associated with this page
		/// </summary>
		private VM mViewModel;

		#endregion

		#region Public Properties

		/// <summary>
		/// The animation the play when the pages is first loaded
		/// </summary>
		public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SliedAndFadeInFromright;

		/// <summary>
		/// The animation the play when the pages is unloaded
		/// </summary>
		public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SliedAndFadeOutToLeft;

		/// <summary>
		/// The time any slide animation takes to complite
		/// </summary>
		public double SlideSeconds { get; set; } = 0.8;

		/// <summary>
		/// The View Model associated with this page
		/// </summary>
		public VM ViewModel
		{
			get => mViewModel;
			set
			{
				// If nosthing has changed, return
				if (mViewModel == value)
					return;

				// Update the value
				mViewModel = value;

				// Set the data context for this page   
				DataContext = mViewModel;
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Default Constructor
		/// </summary>
		public BasePage()
		{
			// If we are animating in, hade to begin with
			if (PageLoadAnimation != PageAnimation.None)
				Visibility = Visibility.Collapsed;

			// Listen out for the page loading
			Loaded += BasePage_LoadedAsync;

			// Create a default view model
			ViewModel = new VM();
		}

		#endregion

		#region Animation Load / Unload

		/// <summary>
		/// Once the page is loaded, parform any required animation
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void BasePage_LoadedAsync(object sender, RoutedEventArgs e)
		{
			// Animate the page in
			await AnimateInAsync();
		}

		/// <summary>
		/// Animates the page in
		/// </summary>
		/// <returns></returns>
		public async Task AnimateInAsync()
		{
			// Make sure we have something to do 
			if (PageLoadAnimation == PageAnimation.None)
				return;

			switch (PageLoadAnimation)
			{
				case PageAnimation.SliedAndFadeInFromright:

					// Satrt the animation
					await this.SlideAndFadeInFromRightAsync(SlideSeconds);

					break;
			}
		}

		/// <summary>
		/// Animates the page out
		/// </summary>
		/// <returns></returns>
		public async Task AnimateOutAsync()
		{
			// Make sure we have something to do 
			if (PageUnloadAnimation == PageAnimation.None)
				return;

			switch (PageUnloadAnimation)
			{
				case PageAnimation.SliedAndFadeOutToLeft:

					// Satrt the animation
					await this.SlideAndFadeOutToLeftAsync(SlideSeconds);

					break;
			}
		}

		#endregion
	}
}