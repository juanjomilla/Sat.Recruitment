using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Constants;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Strategies;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IDictionary<string, IUserTypeStrategy> userTypeStrategies;
        private readonly IRepositoryService _repositoryService;

        public UserService(
            INormalUserStrategy normalUser,
            IPremiumUserStrategy premiumUser,
            ISuperUserStrategy superUser,
            IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
            userTypeStrategies = new Dictionary<string, IUserTypeStrategy>
            {
                { UserTypes.NormalUserType, normalUser },
                { UserTypes.SuperUserType, superUser },
                { UserTypes.PremiumUserType, premiumUser }
            };
        }

        public async Task CreateUserAsync(User user)
        {
            if (userTypeStrategies.TryGetValue(user.UserType?.ToLower(), out IUserTypeStrategy strategy))
            {
                strategy.SetMoney(user);
            }

            await _repositoryService.CreateEntityAsync(user);
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate = null)
        {
            return _repositoryService.GetEntities(predicate);
        }
    }
}
