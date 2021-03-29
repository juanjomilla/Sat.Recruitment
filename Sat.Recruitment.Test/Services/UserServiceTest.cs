using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Strategies;

namespace Sat.Recruitment.Test.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        private static IRepositoryService _repositoryServiceMock;
        private static INormalUserStrategy _normalUser;
        private static IPremiumUserStrategy _premiumUSer;
        private static ISuperUserStrategy _superUser;

        [SetUp]
        public void Initialize()
        {
            _repositoryServiceMock = Substitute.For<IRepositoryService>();
            _normalUser = Substitute.For<INormalUserStrategy>();
            _premiumUSer = Substitute.For<IPremiumUserStrategy>();
            _superUser = Substitute.For<ISuperUserStrategy>();
        }

        [Test]
        public async Task CreateUserAsync_UserTypeNormal_ShouldUseINormalUserStrategy()
        {
            // Arrange
            var userService = new UserService(_normalUser, _premiumUSer, _superUser, _repositoryServiceMock);
            var newUserStub = new User
            {
                UserType = "Normal"
            };

            // Act
            await userService.CreateUserAsync(newUserStub);

            // Assert
            _normalUser.Received().SetMoney(Arg.Any<User>());
            _premiumUSer.DidNotReceive().SetMoney(Arg.Any<User>());
            _superUser.DidNotReceive().SetMoney(Arg.Any<User>());
        }

        [Test]
        public async Task CreateUserAsync_UserTypePremium_ShouldUseIPremiumUserStrategy()
        {
            // Arrange
            var userService = new UserService(_normalUser, _premiumUSer, _superUser, _repositoryServiceMock);
            var newUserStub = new User
            {
                UserType = "PremiumUser"
            };

            // Act
            await userService.CreateUserAsync(newUserStub);

            // Assert
            _premiumUSer.Received().SetMoney(Arg.Any<User>());
            _normalUser.DidNotReceive().SetMoney(Arg.Any<User>());
            _superUser.DidNotReceive().SetMoney(Arg.Any<User>());
        }

        [Test]
        public async Task CreateUserAsync_UserTypeSuperUser_ShouldUseISuperUserStrategy()
        {
            // Arrange
            var userService = new UserService(_normalUser, _premiumUSer, _superUser, _repositoryServiceMock);
            var newUserStub = new User
            {
                UserType = "SuperUser"
            };

            // Act
            await userService.CreateUserAsync(newUserStub);

            // Assert
            _superUser.Received().SetMoney(Arg.Any<User>());
            _premiumUSer.DidNotReceive().SetMoney(Arg.Any<User>());
            _normalUser.DidNotReceive().SetMoney(Arg.Any<User>());
        }

        [Test]
        public async Task CreateUserAsync_UnknownUserType_ShouldNotUseStrategy()
        {
            // Arrange
            var userService = new UserService(_normalUser, _premiumUSer, _superUser, _repositoryServiceMock);
            var newUserStub = new User
            {
                UserType = "NoExistentType"
            };

            // Act
            await userService.CreateUserAsync(newUserStub);

            // Assert
            _superUser.DidNotReceive().SetMoney(Arg.Any<User>());
            _premiumUSer.DidNotReceive().SetMoney(Arg.Any<User>());
            _normalUser.DidNotReceive().SetMoney(Arg.Any<User>());
        }

        [Test]
        public void CreateUserAsync_NullUserType_ShouldThrow_ArgumentNullException()
        {
            // Arrange
            var userService = new UserService(_normalUser, _premiumUSer, _superUser, _repositoryServiceMock);
            var newUserStub = new User();

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.CreateUserAsync(newUserStub));
        }

        [Test]
        public async Task CreateUserAsync_ShouldCall_CreateEntity_FromRepository()
        {
           // Arrange
           var userService = new UserService(_normalUser, _premiumUSer, _superUser, _repositoryServiceMock);
           var newUserStub = new User { UserType = string.Empty };

           // Act
           await userService.CreateUserAsync(newUserStub);

           // Assert
           await _repositoryServiceMock.Received().CreateEntityAsync(Arg.Any<User>());
        }

        [Test]
        public void GetUsers_ShouldCall_GetEntities_FromRepository()
        {
            // Arrange
            var userService = new UserService(_normalUser, _premiumUSer, _superUser, _repositoryServiceMock);

            // Act
            userService.GetUsers(x => string.IsNullOrEmpty(x.Email));

            // Assert
            _repositoryServiceMock.Received().GetEntities(Arg.Any<Func<User, bool>>());
        }

        [Test]
        public void GetUsers_WithPredicate_ShouldCall_GetEntities_WithPredicate_FromRepository()
        {
            // Arrange
            var userService = new UserService(_normalUser, _premiumUSer, _superUser, _repositoryServiceMock);
            Func<User, bool> predicateStub = user => user.Address == string.Empty;

            // Act
            userService.GetUsers(predicateStub);

            // Assert
            _repositoryServiceMock.Received().GetEntities(predicateStub);
        }

        [Test]
        public void GetUsers_ShouldReturnUsers()
        {
            // Arrange
            var userService = new UserService(_normalUser, _premiumUSer, _superUser, _repositoryServiceMock);
            _repositoryServiceMock.GetEntities<User>().Returns(Enumerable.Empty<User>());

            // Act
            var result = userService.GetUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<User>>(result);
        }
    }
}
