using System;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Strategies
{
    public class SuperUserStrategy : ISuperUserStrategy
    {
        private readonly double Upper100USDMultiplier = 1.2;
        private readonly double DefaultMultiplier = 1;

        public void SetMoney(User user)
        {
            var percentage = user.Money > 100 ? Upper100USDMultiplier : DefaultMultiplier;

            user.Money *= Convert.ToDecimal(percentage);
        }
    }
}
