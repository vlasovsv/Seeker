namespace Seeker.Actors
{
    /// <summary>
    /// Represents actor's meatadata.
    /// </summary>
    public class ActorMetaData
    {
        #region Private fields

        private readonly string _name;
        private readonly ActorMetaData _parent;
        private readonly string _path;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates actor's metadata.
        /// </summary>
        /// <param name="name">Actor's name.</param>
        /// <param name="parent">Actor's parent.</param>
        public ActorMetaData(string name, ActorMetaData parent = null)
        {
            _name = name;
            _parent = parent;
            var parentPath = parent != null ? parent.Path : "/user";
            _path = string.Format("{0}/{1}", parentPath, Name);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets actor's name.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets actor's parent.
        /// </summary>
        public ActorMetaData Parent
        {
            get
            {
                return _parent;
            }
        }

        /// <summary>
        /// Gets a path to the actor.
        /// </summary>
        public string Path
        {
            get
            {
                return _path;
            }
        }

        #endregion
    }
}
