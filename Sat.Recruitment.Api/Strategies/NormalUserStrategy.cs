using System;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Strategies
{
    public class NormalUserStrategy : INormalUserStrategy
    {
        private readonly double Upper100USDMultiplier = 1.12;
        private readonly double Between10And100USDMultiplier = 1.8;
        private readonly double DefaultMultiplier = 1;

        public void SetMoney(User user)
        {
            var percentage = user.Money > 100 ? Upper100USDMultiplier : (user.Money < 100 && user.Money > 10) ? Between10And100USDMultiplier : DefaultMultiplier;

            user.Money *= Convert.ToDecimal(percentage);
        }
    }
}
