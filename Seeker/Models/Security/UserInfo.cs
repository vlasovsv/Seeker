using System;

namespace Seeker.Models.Security
{
    /// <summary>
    /// Represents a user info.
    /// </summary>
    public sealed class UserInfo
    {
        #region Private fields

        private readonly Guid _id;
        private readonly string _userName;
        private readonly string _passwordHash;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new user info.
        /// </summary>
        /// <param name="id">An user identifier.</param>
        /// <param name="userName">A user name.</param>
        /// <param name="passwordHash">A user password hash.</param>
        public UserInfo(Guid id, string userName, string passwordHash)
        {
            _id = id;
            _userName = userName;
            _passwordHash = passwordHash;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }
        }

        /// <summary>
        /// Gets the user password hash.
        /// </summary>
        public string PasswordHash
        {
            get
            {
                return _passwordHash;
            }
        }

        #endregion
    }
}
