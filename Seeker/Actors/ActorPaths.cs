namespace Seeker.Actors
{
    /// <summary>
    /// Repesents actor paths.
    /// </summary>
    public static class ActorPaths
    {
        public static readonly ActorMetaData Listener = new ActorMetaData("socket-listener");

        public static readonly ActorMetaData ProcessorManager = new ActorMetaData("processor-manager");

        public static readonly ActorMetaData Indexer = new ActorMetaData("indexer");
    }
}
