namespace Seeker.Models.Security
{
    /// <summary>
    /// Represents a login model.
    /// </summary>
    public sealed class LoginModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        public string UserPassword
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value indicating whether this user to be remembered.
        /// </summary>
        public bool RememberMe
        {
            get;
            set;
        }

        #endregion
    }
}
