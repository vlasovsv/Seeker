using Akka.Actor;
using Autofac;
using NLog.LayoutRenderers;
using Topshelf;
using Topshelf.Logging;
using Topshelf.Nancy;

using Seeker.Configuration;
using Seeker.Searching;
using Seeker.Actors;

namespace Seeker
{
    class Program
    {
        #region Methods

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            ConfigureContainer(builder);
            var container = builder.Build();
            AutofacContext.Container = container;
            ConfigureLog();
            var host = ConfigureHost(container);
            host.Run();
        }

        /// <summary>
        /// Configures a service.
        /// </summary>
        /// <param name="container">An Autofac container.</param>
        /// <returns>
        /// Returns a host instance.
        /// </returns>
        private static Host ConfigureHost(IContainer container)
        {
            var settings = container.Resolve<ISeekerSettings>();

            var host = HostFactory.New(x =>
            {
                x.EnableShutdown();
                x.RunAsLocalSystem();
                x.SetServiceName("Seeker");
                x.SetDisplayName("Seeker");
                x.SetDescription("Collects log messages");
                x.StartAutomatically();

                x.Service<SeekerService>(cfg =>
                {
                    cfg.ConstructUsing(() => container.Resolve<SeekerService>());
                    cfg.WhenStarted<SeekerService>(srv => srv.Start());
                    cfg.WhenStopped<SeekerService>(srv => srv.Stop());

                    cfg.WithNancyEndpoint(x, c =>
                    {
                        c.AddHost(port: settings.Port);
                        c.CreateUrlReservationsOnInstall();
                        c.Bootstrapper = new SeekerBootstrapper(AutofacContext.Container);
                    });
                });
                x.EnableServiceRecovery(cfg => cfg.RestartService(1));
                x.UseNLog();
                x.OnException(ex => HostLogger.Get("Seeker")
                    .Error("Unhandled exception.", ex));
            });
            return host;
        }

        /// <summary>
        /// Configures a DI-container.
        /// </summary>
        /// <param name="builder">A container builder.</param>
        private static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<SeekerService>();

            builder.RegisterType<AppConfigSettings>().As<ISeekerSettings>();
            builder.Register<LuceneWrapper>(c => new LuceneWrapper(c.Resolve<ISeekerSettings>().Store)).AsSelf().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(ActorPaths).Assembly)
                .Where(x => typeof(ActorBase).IsAssignableFrom(x)).AsSelf();
        }

        /// <summary>
        /// Configures a logger.
        /// </summary>
        private static void ConfigureLog()
        {
            LayoutRenderer.Register("store", log => AutofacContext.Container.Resolve<ISeekerSettings>().Store);
        }

        #endregion
    }
}
