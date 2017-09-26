namespace Seeker.Messages
{
    /// <summary>
    /// Represents a user logged in event.
    /// </summary>
    public sealed class UserLoggedInMessage
    {
        #region Private fields

        private readonly string _userName;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of a user logged in message;
        /// </summary>
        /// <param name="userName">The user name.</param>
        public UserLoggedInMessage(string userName)
        {
            _userName = userName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the user name.
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }
        }

        #endregion
    }
}
