namespace Seeker.Models.Pages
{
    /// <summary>
    /// Represents a sign in page model.
    /// </summary>
    public class ErrorSignInModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether or not the error occured during sign in.
        /// </summary>
        public bool Error
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the username that was used to sign in.
        /// </summary>
        public string Username
        {
            get;
            set;
        }

        #endregion
    }
}
