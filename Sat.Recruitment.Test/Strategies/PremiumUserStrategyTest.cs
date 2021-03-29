using NUnit.Framework;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Strategies;

namespace Sat.Recruitment.Test.Strategies
{
    [TestFixture]
    public class PremiumUserStrategyTest
    {
        [Test]
        public void SetMoney_ShouldUse_Upper100USDMultiplier()
        {
            // Arrange
            var premiumUserStrategy = new PremiuUserStrategy();
            var userMoney = 150;
            var userMock = new User { Money = userMoney };
            var expectedMoney = userMoney * 3;

            // Act
            premiumUserStrategy.SetMoney(userMock);

            // Arrange
            Assert.AreEqual(expectedMoney, userMock.Money);
        }

        [Test]
        public void SetMoney_ShouldUse_DefaultMultiplier()
        {
            // Arrange
            var premiumUserStrategy = new PremiuUserStrategy();
            var userMoney = 8;
            var userMock = new User { Money = userMoney };
            var expectedMoney = userMoney * 1;

            // Act
            premiumUserStrategy.SetMoney(userMock);

            // Arrange
            Assert.AreEqual(expectedMoney, userMock.Money);
        }
    }
}
