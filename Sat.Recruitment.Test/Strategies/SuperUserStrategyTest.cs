﻿using System;
using NUnit.Framework;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Strategies;

namespace Sat.Recruitment.Test.Strategies
{
    [TestFixture]
    public class SuperUserStrategyTest
    {
        [Test]
        public void SetMoney_ShouldUse_Upper100USDMultiplier()
        {
            // Arrange
            var superUserStrategy = new SuperUserStrategy();
            var userMoney = 150;
            var userMock = new User { Money = userMoney };
            var expectedMoney = Convert.ToDecimal(userMoney * 1.2);

            // Act
            superUserStrategy.SetMoney(userMock);

            // Arrange
            Assert.AreEqual(expectedMoney, userMock.Money);
        }

        [Test]
        public void SetMoney_ShouldUse_DefaultMultiplier()
        {
            // Arrange
            var superUserStrategy = new SuperUserStrategy();
            var userMoney = 8;
            var userMock = new User { Money = userMoney };
            var expectedMoney = Convert.ToDecimal(userMoney * 1);

            // Act
            superUserStrategy.SetMoney(userMock);

            // Arrange
            Assert.AreEqual(expectedMoney, userMock.Money);
        }
    }
}