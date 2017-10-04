using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

using Nancy;
using Newtonsoft.Json;

using Seeker.Configuration;

namespace Seeker.Models.Security
{
    /// <summary>
    /// Represents a user manager.
    /// </summary>
    public sealed class UserManager : IUserManager
    {
        #region Private fields

        private readonly ISeekerSettings _settings;
        private string _userFileLocation;
        private List<UserInfo> _users;
        private object _locker = new object();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new user manager.
        /// </summary>
        /// <param name="settings">Seeker settngs.</param>
        public UserManager(ISeekerSettings settings)
        {
            _settings = settings;
            _userFileLocation = Path.Combine(_settings.Security, "users.json");
            _users = new List<UserInfo>();
            ReloadUsers();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the real username from an identifier.
        /// </summary>
        /// <param name="identifier">User identifier.</param>
        /// <param name="context">The current NancyFx context.</param>
        /// <returns>
        /// Matching populated IUserIdentity object, or empty.
        /// </returns>
        public ClaimsPrincipal GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var userInfo = _users.FirstOrDefault(x => x.ID == identifier);
            if (userInfo != null)
            {
                return new ClaimsPrincipal(new GenericIdentity(userInfo.UserName));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <param name="password">User password.</param>
        public void Register(string userName, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var userInfo = new UserInfo(Guid.NewGuid(), userName, passwordHash);
            _users.Add(userInfo);
            StoreUsers();
        }

        /// <summary>
        /// Validates a pair of username and password.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <param name="password">User password.</param>
        /// <returns>
        /// If this security pair is valid returns a user identifier, otherwise null.
        /// </returns>
        public Guid? Validate(string userName, string password)
        {
            var userInfo = _users.FirstOrDefault(x => x.UserName == userName 
                && BCrypt.Net.BCrypt.Verify(password, x.PasswordHash));

            if (userInfo != null)
            {
                return userInfo.ID;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Reloads users.
        /// </summary>
        private void ReloadUsers()
        {
            if (File.Exists(_userFileLocation))
            {
                lock (_locker)
                {
                    using (StreamReader sr = File.OpenText(_userFileLocation))
                    {
                        using (JsonReader reader = new JsonTextReader(sr))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            _users = serializer.Deserialize<List<UserInfo>>(reader);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Stores users.
        /// </summary>
        private void StoreUsers()
        {
            lock (_locker)
            {
                if (!Directory.Exists(_settings.Security))
                {
                    Directory.CreateDirectory(_settings.Security);
                }

                using (StreamWriter sw = File.CreateText(_userFileLocation))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(sw, _users);
                }
            }
        }

        #endregion
    }
}
