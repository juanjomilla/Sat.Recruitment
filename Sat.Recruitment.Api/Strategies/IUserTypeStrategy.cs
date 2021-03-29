using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Strategies
{
    public interface IUserTypeStrategy
    {
        void SetMoney(User user);
    }
}
