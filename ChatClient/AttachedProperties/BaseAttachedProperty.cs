using System;
using System.Windows;

namespace ChatClient
{
	/// <summary>
	/// A Base Attached Property to replace a vanila WPF Attached Property
	/// </summary>
	/// <typeparam name="Parent">The parent class to be the attached property</typeparam>
	/// <typeparam name="Property">The type of this attached property</typeparam>
	public abstract class BaseAttachedProperty<Parent, Property>
		where Parent : BaseAttachedProperty<Parent, Property>, new()
	{
		#region Public Eventsa

		/// <summary>
		/// Fired when value changes
		/// </summary>
		public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

		/// <summary>
		/// Fired when value changes,even when the value is the same
		/// </summary>
		public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };

		#endregion

		#region Public properties

		/// <summary>
		/// A singleton instance of our parent class
		/// </summary>
		public static Parent Instance { get; private set; } = new Parent();

		#endregion

		#region Attached property defenirions

		/// <summary>
		/// The attached property for this class 
		/// </summary>
		public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
			"Value", 
			typeof(Property), 
			typeof(BaseAttachedProperty<Parent, Property>), 
			new PropertyMetadata(
				default(Property),
				new PropertyChangedCallback(OnValuePropertyChanged),
				new CoerceValueCallback(OnValuePropertyUpdated)));

		/// <summary>
		/// The callback event when <see cref="ValueProperty"/> is changed
		/// </summary>
		/// <param name="d">The UI element that had it's property changed</param>
		/// <param name="e">The arguments for the event</param>
		private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			// Call the parent function
			Instance.OnValueChanged(d, e);

			// Call event instances
			Instance.ValueChanged(d, e);
		}

		/// <summary>
		/// The callback event when <see cref="ValueProperty"/> is changed,evevn if it is the same value
		/// </summary>
		/// <param name="d">The UI element that had it's property changed</param>
		/// <param name="value">The arguments for the event</param>
		private static object OnValuePropertyUpdated(DependencyObject d, object value)
		{
			// Call the parent function
			Instance.OnValueUpdated(d, value);

			// Call event instances
			Instance.ValueUpdated(d, value);

			// Return the value
			return value;
		}

		/// <summary>
		/// Get's the attached property
		/// </summary>
		/// <param name="d">The alement to get th eproperty from</param>
		/// <returns></returns>
		public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);

		/// <summary>
		/// Sets the attached property
		/// </summary>
		/// <param name="d">The alement to get th eproperty from</param>
		/// <param name="value">The value ro set the property to</param>
		public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);

		#endregion

		#region Event Methods

		/// <summary>
		/// The method that is called when any attached property of this type is changed
		/// </summary>
		/// <param name="sender">The UI element that this property was cahnged for</param>
		/// <param name="e">The arguments for this event</param>
		public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

		/// <summary>
		/// The method that is called when any attached property of this type is changed,even if the value is the same
		/// </summary>
		/// <param name="sender">The UI element that this property was cahnged for</param>
		/// <param name="value">The arguments for this event</param>
		public virtual void OnValueUpdated(DependencyObject sender, object value) { }

		#endregion
	}
}