using System;
using NUnit.Framework;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Strategies;

namespace Sat.Recruitment.Test.Strategies
{
    [TestFixture]
    public class NormalUserStrategyTest
    {
        [Test]
        public void SetMoney_ShouldUse_Upper100USDMultiplier()
        {
            // Arrange
            var normalUserStrategy = new NormalUserStrategy();
            var userMoney = 150;
            var userMock = new User { Money = userMoney };
            var expectedMoney = Convert.ToDecimal(userMoney * 1.12);

            // Act
            normalUserStrategy.SetMoney(userMock);

            // Arrange
            Assert.AreEqual(expectedMoney, userMock.Money);
        }

        [Test]
        public void SetMoney_ShouldUse_Between10And100USDMultiplier()
        {
            // Arrange
            var normalUserStrategy = new NormalUserStrategy();
            var userMoney = 88;
            var userMock = new User { Money = userMoney };
            var expectedMoney = Convert.ToDecimal(userMoney * 1.8);

            // Act
            normalUserStrategy.SetMoney(userMock);

            // Arrange
            Assert.AreEqual(expectedMoney, userMock.Money);
        }

        [Test]
        public void SetMoney_ShouldUse_DefaultMultiplier()
        {
            // Arrange
            var normalUserStrategy = new NormalUserStrategy();
            var userMoney = 2;
            var userMock = new User { Money = userMoney };
            var expectedMoney = Convert.ToDecimal(userMoney * 1);

            // Act
            normalUserStrategy.SetMoney(userMock);

            // Arrange
            Assert.AreEqual(expectedMoney, userMock.Money);
        }
    }
}
