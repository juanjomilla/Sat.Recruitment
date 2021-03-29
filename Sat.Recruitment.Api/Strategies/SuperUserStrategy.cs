using System;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Strategies
{
    public class SuperUserStrategy : ISuperUserStrategy
    {
        private readonly double Upper100USDGiftMultiplier = 1.2;
        private readonly double DefaultGiftMultiplier = 1;

        public void SetMoney(User user)
        {
            var percentage = user.Money > 100 ? Upper100USDGiftMultiplier : DefaultGiftMultiplier;

            user.Money *= Convert.ToDecimal(percentage);
        }
    }
}
