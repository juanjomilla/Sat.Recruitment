using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);

        IEnumerable<User> GetUsers(Func<User, bool> predicate = null);
    }
}
