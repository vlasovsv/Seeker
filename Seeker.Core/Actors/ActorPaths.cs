namespace Seeker.Actors
{
    /// <summary>
    /// Repesents actor paths.
    /// </summary>
    public static class ActorPaths
    {
        public static readonly ActorMetaData UserManager = new ActorMetaData("user-manager");

        public static readonly ActorMetaData MessageIngestor = new ActorMetaData("message-ingestor");

        public static readonly ActorMetaData Indexer = new ActorMetaData("log-indexer");

        public static readonly ActorMetaData QueryStatistics = new ActorMetaData("query-statistics");
    }
}
