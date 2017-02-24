using Autofac;

namespace Seeker
{
    /// <summary>
    /// Represents an Autofac container.
    /// </summary>
    public static class AutofacContext
    {
        #region Properties

        /// <summary>
        /// Gets or sets an Autofac container.
        /// </summary>
        public static IContainer Container
        {
            get;
            set;
        }

        #endregion
    }
}
