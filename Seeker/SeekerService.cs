using System.Net;

using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.AutoFac;
using Topshelf.Logging;

using Seeker.Actors;
using Seeker.Configuration;

namespace Seeker
{
    /// <summary>
    /// Represents a seeker service.
    /// </summary>
    public sealed class SeekerService
    {
        #region Private fields

        private ActorSystem _system;
        private readonly LogWriter _seekerLogger = HostLogger.Get("Seeker");
        private readonly ISeekerSettings _settings;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a seeker service.
        /// </summary>
        /// <param name="settings">Seeker settings.</param>
        public SeekerService(ISeekerSettings settings)
        {
            _settings = settings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts the service.
        /// </summary>
        public void Start()
        {
            _seekerLogger.Info("Запуск сервера приёма сообщений.");
            _system = ActorSystem.Create("seeker-system");
            var resolver = new AutoFacDependencyResolver(AutofacContext.Container, _system);
            InitializeActors();
            _seekerLogger.Info("Сервер приёма сообщений запущен.");
        }

        /// <summary>
        /// Initialize actors.
        /// </summary>
        private void InitializeActors()
        {
            var listener = _system.ActorOf(Props.Create(() => new SocketListener(
                new IPEndPoint(IPAddress.Any, _settings.Port))), ActorNames.Listener);
            var processorManager = _system.ActorOf<ProcessorManager>(ActorNames.ProcessorManager);
            var indexer = _system.ActorOf(_system.DI().Props<Indexer>(), ActorNames.Indexer);
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        public async void Stop()
        {
            _seekerLogger.Info("Остановка сервера приёма сообщений.");
            await _system.Terminate();
            _seekerLogger.Info("Сервер приёма сообщений остановлен.");
        }

        #endregion
    }
}