using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ChatClient
{
	/// <summary>
	/// Helpers to animate pages in specific ways
	/// </summary>
	public static class PageAnimations
	{
		/// <summary>
		/// Slides a page in from the right
		/// </summary>
		/// <param name="page">The page to animate</param>
		/// <param name="seconds">the time the animation will take</param>
		/// <returns></returns>
		public static async Task SlideAndFadeInFromRightAsync(this Page page, double seconds)
		{
			// Create the storyboard
			var sb = new Storyboard();

			// Add slide from right animation
			sb.AddSlideFromRight(seconds, page.WindowWidth);

			// Add fade in animation
			sb.AddFadeIn(seconds);

			// Start animationg
			sb.Begin(page);

			// Make page visible
			page.Visibility = Visibility.Visible;

			// Wait it for finish
			await Task.Delay((int)(seconds * 1000));
		}

		/// <summary>
		/// Slides a page out to the left
		/// </summary>
		/// <param name="page">The page to animate</param>
		/// <param name="seconds">the time the animation will take</param>
		/// <returns></returns>
		public static async Task SlideAndFadeOutToLeftAsync(this Page page, double seconds)
		{
			// Create the storyboard
			var sb = new Storyboard();

			// Add slide from right animation
			sb.AddSlideToLeft(seconds, page.WindowWidth);

			// Add fade in animation
			sb.AddFadeOut(seconds);

			// Start animationg
			sb.Begin(page);

			// Make page visible
			page.Visibility = Visibility.Visible;

			// Wait it for finish
			await Task.Delay((int)(seconds * 1000));
		}
	}
}