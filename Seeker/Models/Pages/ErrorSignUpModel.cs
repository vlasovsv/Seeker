namespace Seeker.Models.Pages
{
    /// <summary>
    /// Represents a sign up page model.
    /// </summary>
    public class ErrorSignUpModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether or not the error occured during sign up.
        /// </summary>
        public bool Error
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage
        {
            get;
            set;
        }

        #endregion
    }
}
