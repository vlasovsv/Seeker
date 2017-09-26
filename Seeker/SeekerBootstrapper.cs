using Autofac;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;

using Seeker.Models.Security;

namespace Seeker
{
    /// <summary>
    /// Represents a seeker bootstrapper.
    /// </summary>
    public sealed class SeekerBootstrapper : AutofacNancyBootstrapper
    {
        #region Private fields

        private readonly IContainer _originalContainer;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of Nancy bootstrapper.
        /// </summary>
        /// <param name="container"></param>
        public SeekerBootstrapper(IContainer container)
        {
            _originalContainer = container;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a pre-existing container instance in order to share common dependencies.
        /// </summary>
        /// <returns>
        /// The Autofac container.
        /// </returns>
        protected override ILifetimeScope GetApplicationContainer()
        {
            return _originalContainer;
        }

        protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Update(cb => cb.RegisterType<UserManager>().AsImplementedInterfaces());
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            FormsAuthentication.Enable(pipelines,
                new FormsAuthenticationConfiguration()
                {
                    RedirectUrl = "~/signin",
                    UserMapper = container.Resolve<IUserMapper>()
                });
        }

        #endregion
    }
}
