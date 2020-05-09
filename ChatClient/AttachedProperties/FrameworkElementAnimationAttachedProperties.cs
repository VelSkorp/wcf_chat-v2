﻿using System.Windows;

namespace ChatClient
{
	/// <summary>
	/// A base class to run any animation mathod when a boolean is set to true
	/// and reverse animation when set to false
	/// </summary>
	/// <typeparam name="Parent"></typeparam>
	public abstract class AnimateBaseProperty<Parent> : BaseAttachedProperty<Parent, bool>
		where Parent : BaseAttachedProperty<Parent, bool>, new()
	{
		#region Public Properties

		/// <summary>
		/// A flag indicating if this is the first time this property has been loaded
		/// </summary>
		public bool FirstLoad { get; set; } = true;

		#endregion

		public override void OnValueUpdated(DependencyObject sender, object value)
		{
			// Get the framework element
			if (!(sender is FrameworkElement element))
				return;

			// Gon't fire is the value doesn't changed
			if (sender.GetValue(ValueProperty) == value && !FirstLoad)
				return;

			// On first load
			if (FirstLoad)
			{
				// Create a single self-unhookable event
				// for the elements Loaded event
				RoutedEventHandler onLoaded = null;
				onLoaded = (ss, ee) =>
				{
					// Unhook ourselves
					element.Loaded -= onLoaded;

					// Do desired animation
					DoAnimation(element, (bool)value);

					// No longer in first load
					FirstLoad = false;
				};

				// Hook into the loaded event of the element
				element.Loaded += onLoaded;
			}
			else
				// Do desired animation
				DoAnimation(element, (bool)value);
		}

		/// <summary>
		/// The aniamtion method that is fired when the value changes
		/// </summary>
		/// <param name="element">The element</param>
		/// <param name="value">The new value</param>
		protected virtual void DoAnimation(FrameworkElement element, bool value) { }
	}

	/// <summary>
	/// Animates a framework element sliding it in from left on show
	/// and sliding out to the left om hide
	/// </summary>
	public class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty>
	{
		protected override async void DoAnimation(FrameworkElement element, bool value)
		{
			if (value)
				// Animate in
				await element.SlideAndFadeInFromLeftAsync(FirstLoad ? 0 : 0.3, keepMargin: false);
			else
				// Animate out
				await element.SlideAndFadeOutToLeftAsync(FirstLoad ? 0 : 0.3, keepMargin: false);
		}
	}
}