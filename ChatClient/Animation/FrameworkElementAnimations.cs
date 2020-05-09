using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace ChatClient
{
	/// <summary>
	/// Helpers to animate framework elements in specific ways
	/// </summary>
	public static class FrameworkElementAnimations
	{
		/// <summary>
		/// Slides an element in from the right
		/// </summary>
		/// <param name="element">The element to animate</param>
		/// <param name="seconds">The time the animation will take</param>
		/// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
		/// <returns></returns>
		public static async Task SlideAndFadeInFromRightAsync(this FrameworkElement element, double seconds = 0.3, bool keepMargin = true)
		{
			// Create the storyboard
			var sb = new Storyboard();

			// Add slide from right animation
			sb.AddSlideFromRight(seconds, element.ActualWidth, keepMargin: keepMargin);

			// Add fade in animation
			sb.AddFadeIn(seconds);

			// Start animationg
			sb.Begin(element);

			// Make page visible
			element.Visibility = Visibility.Visible;

			// Wait it for finish
			await Task.Delay((int)(seconds * 1000));
		}

		/// <summary>
		/// Slides an element in from the right
		/// </summary>
		/// <param name="element">The element to animate</param>
		/// <param name="seconds">The time the animation will take</param>
		/// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
		/// <returns></returns>
		public static async Task SlideAndFadeInFromLeftAsync(this FrameworkElement element, double seconds = 0.3, bool keepMargin = true)
		{
			// Create the storyboard
			var sb = new Storyboard();

			// Add slide from right animation
			sb.AddSlideFromLeft(seconds, element.ActualWidth, keepMargin: keepMargin);

			// Add fade in animation
			sb.AddFadeIn(seconds);

			// Start animationg
			sb.Begin(element);

			// Make page visible
			element.Visibility = Visibility.Visible;

			// Wait it for finish
			await Task.Delay((int)(seconds * 1000));
		}

		/// <summary>
		/// Slides a element out to the left
		/// </summary>
		/// <param name="element">The element to animate</param>
		/// <param name="seconds">The time the animation will take</param>
		/// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
		/// <returns></returns>
		public static async Task SlideAndFadeOutToLeftAsync(this FrameworkElement element, double seconds = 0.3, bool keepMargin = true)
		{
			// Create the storyboard
			var sb = new Storyboard();

			// Add slide from right animation
			sb.AddSlideToLeft(seconds, element.ActualWidth, keepMargin: keepMargin);

			// Add fade in animation
			sb.AddFadeOut(seconds);

			// Start animationg
			sb.Begin(element);

			// Make page visible
			element.Visibility = Visibility.Visible;

			// Wait it for finish
			await Task.Delay((int)(seconds * 1000));
		}

		/// <summary>
		/// Slides a element out to the right
		/// </summary>
		/// <param name="element">The element to animate</param>
		/// <param name="seconds">The time the animation will take</param>
		/// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
		/// <returns></returns>
		public static async Task SlideAndFadeOutToRightAsync(this FrameworkElement element, double seconds = 0.3, bool keepMargin = true)
		{
			// Create the storyboard
			var sb = new Storyboard();

			// Add slide from right animation
			sb.AddSlideToRight(seconds, element.ActualWidth, keepMargin: keepMargin);

			// Add fade in animation
			sb.AddFadeOut(seconds);

			// Start animationg
			sb.Begin(element);

			// Make page visible
			element.Visibility = Visibility.Visible;

			// Wait it for finish
			await Task.Delay((int)(seconds * 1000));
		}
	}
}