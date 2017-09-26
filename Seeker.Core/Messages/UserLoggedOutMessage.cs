namespace Seeker.Messages
{
    /// <summary>
    /// Represents an user logged out event.
    /// </summary>
    public class UserLoggedOutMessage
    {
        #region Private fields

        private readonly string _userName;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of a user logged out message;
        /// </summary>
        /// <param name="userName">The user name.</param>
        public UserLoggedOutMessage(string userName)
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
