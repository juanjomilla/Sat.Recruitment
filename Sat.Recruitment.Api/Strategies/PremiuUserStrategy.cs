using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Strategies
{
    public class PremiuUserStrategy : IPremiumUserStrategy
    {
        private readonly decimal Upper100USDGiftMultiplier = 3;
        private readonly decimal DefaultGiftMultiplier = 1;

        public void SetMoney(User user)
        {
            var percentage = user.Money > 100 ? Upper100USDGiftMultiplier : DefaultGiftMultiplier;

            user.Money *= percentage;
        }
    }
}
