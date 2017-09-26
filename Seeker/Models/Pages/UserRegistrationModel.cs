namespace Seeker.Models.Pages
{
    /// <summary>
    /// Represents a user registration.
    /// </summary>
    public sealed class UserRegistrationModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets a user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets a user password.
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// Gets or sets a confirmed user password.
        /// </summary>
        public string ConfirmedUserPassword { get; set; }

        #endregion
    }
}
