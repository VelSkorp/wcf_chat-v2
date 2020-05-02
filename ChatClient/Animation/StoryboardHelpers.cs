﻿using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace ChatClient
{
    /// <summary>
    /// Animation helpers for <see cref="StoryBoard"/>
    /// </summary>
    public static class StoryboardHelpers
    {
        /// <summary>
        /// Adds a slide from right animetion to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="offset">The distance to the right to start from</param>
        /// <param name="deecelerationRatio">The rate of deceleration</param>
        public static void AddSlideFromRight(this Storyboard storyboard, double seconds, double offset, double decelerationRatio = 0.9)
        {
            // Create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(offset, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio,
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storeyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a slide to left animetion to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="offset">The distance to the right to start from</param>
        /// <param name="deecelerationRatio">The rate of deceleration</param>
        public static void AddSlideToLeft(this Storyboard storyboard, double seconds, double offset, double decelerationRatio = 0.9)
        {
            // Create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(-offset, 0, offset, 0),
                DecelerationRatio = decelerationRatio,
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storeyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a fade in animetion to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddFadeIn(this Storyboard storyboard, double seconds)
        {
            // Create the margin animate from right
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 0,
                To = 1,
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // Add this to the storeyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a fade out animetion to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddFadeOut(this Storyboard storyboard, double seconds)
        {
            // Create the margin animate from right
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = 0,
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // Add this to the storeyboard
            storyboard.Children.Add(animation);
        }        
    }
}