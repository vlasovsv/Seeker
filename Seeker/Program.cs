using Akka.Actor;
using Autofac;
using NLog.LayoutRenderers;
using Topshelf;
using Topshelf.Logging;

using Seeker.Configuration;
using Seeker.Searching;

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
            var host = HostFactory.New(x =>
            {
                x.EnableShutdown();
                x.RunAsLocalSystem();
                x.SetServiceName("Seeker");
                x.SetDisplayName("Сервис сборщика логов");
                x.SetDescription("Отвечает за приём лог-сообщений с различных клиентов");
                x.StartAutomatically();
                
                x.Service<SeekerService>(cfg =>
                {
                    cfg.ConstructUsing(() => container.Resolve<SeekerService>());
                    cfg.WhenStarted<SeekerService>(srv => srv.Start());
                    cfg.WhenStopped<SeekerService>(srv => srv.Stop());
                });
                x.EnableServiceRecovery(cfg => cfg.RestartService(1));
                x.UseNLog();
                x.OnException(ex => HostLogger.Get("Seeker")
                    .Error("Необработанное исключение.", ex));
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
            builder.RegisterType<LuceneWrapper>().AsSelf().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
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
