namespace ChatClient
{
    /// <summary>
    /// Styles of page animation for appearing/disappearing
    /// </summary>
    public  enum PageAnimation
    {
        /// <summary>
        /// No animation takes place 
        /// </summary>
        None=0,

        /// <summary>
        /// The page slides and fades in from the right
        /// </summary>
        SliedAndFadeInFromright = 1,

        /// <summary>
        /// The page slides and fades out to the left
        /// </summary>
        SliedAndFadeOutToLeft = 2,
    }
}