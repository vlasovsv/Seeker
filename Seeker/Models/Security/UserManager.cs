using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

using Nancy;
using Seeker.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Principal;

namespace Seeker.Models.Security
{
    public sealed class UserManager : IUserManager
    {
        #region Private fields

        private readonly ISeekerSettings _settings;
        private string _userFileLocation;
        private List<UserInfo> _users;
        private object _locker = new object();

        #endregion

        #region Constructors

        public UserManager(ISeekerSettings settings)
        {
            _settings = settings;
            _userFileLocation = Path.Combine(_settings.Security, "users.json");
            _users = new List<UserInfo>();
            ReloadUsers();
        }

        #endregion

        #region Methods

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

        public void Register(string userName, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var userInfo = new UserInfo(Guid.NewGuid(), userName, passwordHash);
            _users.Add(userInfo);
            StoreUsers();
        }

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
