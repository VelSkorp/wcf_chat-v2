namespace ChatClient.Core
{
    /// <summary>
    /// A view model for each chat list item in the overview chat list
    /// </summary>
    public class ChatListItemViewModel : BaseViewModel
    {
        /// <summary>
        /// The display name of this chat list
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The latest message form this chat
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The initials for the profile picture background
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// The RGB values (in hex) for the backgorund color of the profile picture
        /// For example FF00FF for Red and Blue mixed
        /// </summary>
        public string ProfilePictureRGB { get; set; }

        /// <summary>
        /// True is there are unread message in this chat
        /// </summary>
        public bool NewContentAvalible { get; set; }

        /// <summary>
        /// True if this item is curently selected
        /// </summary>
        public bool IsSelected { get; set; }
    }
}