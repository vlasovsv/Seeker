using System;

using Nancy.Authentication.Forms;

namespace Seeker.Models.Security
{
    /// <summary>
    /// Represents a user manager interface.
    /// </summary>
    public interface IUserManager : IUserMapper
    {
        Guid? Validate(string userName, string password);

        void Register(string userName, string password);
    }
}
