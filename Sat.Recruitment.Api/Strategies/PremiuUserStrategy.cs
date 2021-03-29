using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Strategies
{
    public class PremiuUserStrategy : IPremiumUserStrategy
    {
        private readonly decimal Upper100USDMultiplier = 3;
        private readonly decimal DefaultMultiplier = 1;

        public void SetMoney(User user)
        {
            var percentage = user.Money > 100 ? Upper100USDMultiplier : DefaultMultiplier;

            user.Money *= percentage;
        }
    }
}
