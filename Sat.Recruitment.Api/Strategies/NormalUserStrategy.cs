using System;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Strategies
{
    public class NormalUserStrategy : INormalUserStrategy
    {
        private readonly double Upper100USDGiftMultiplier = 1.12;
        private readonly double Between10And100USDGiftMultiplier = 1.8;
        private readonly double DefaultGiftMultiplier = 1;

        public void SetMoney(User user)
        {
            var percentage = user.Money > 100 ? Upper100USDGiftMultiplier : (user.Money < 100 && user.Money > 10) ? Between10And100USDGiftMultiplier : DefaultGiftMultiplier;

            user.Money *= Convert.ToDecimal(percentage);
        }
    }
}
