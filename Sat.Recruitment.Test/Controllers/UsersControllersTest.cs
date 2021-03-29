using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Request;
using Sat.Recruitment.Api.Models.Response;
using Sat.Recruitment.Api.Services;

namespace Sat.Recruitment.Test.Controllers
{
    [TestFixture]
    public class UsersControllersTest
    {
        private static IUserService _userServiceMock;

        [SetUp]
        public void Initialize()
        {
            _userServiceMock = Substitute.For<IUserService>();
        }

        [Test]
        public async Task CreateUser_DuplicatedUser_ShouldReturnBadRequest()
        {
            // Arrange
            var usersController = new UsersController(_userServiceMock);
            var requestStub = GetUserRequestStub();
            _userServiceMock
                .GetUsers(Arg.Any<Func<User, bool>>())
                .Returns(new List<User> { new User() });

            // Act
            var result = await usersController.CreateUser(requestStub);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CreateUser_DuplicatedUser_ShouldReturn_UserDuplicatedResult()
        {
            // Arrange
            var usersController = new UsersController(_userServiceMock);
            var requestStub = GetUserRequestStub();
            var expectedErrorMessage = "The user is duplicated";
            _userServiceMock
                .GetUsers(Arg.Any<Func<User, bool>>())
                .Returns(new List<User> { new User() });

            // Act
            var result = await usersController.CreateUser(requestStub);

            // Assert
            var objectResult = (BadRequestObjectResult)result;
            var responseResult = (Result)objectResult.Value;
            Assert.AreEqual(expectedErrorMessage, responseResult.Errors);
            Assert.IsFalse(responseResult.IsSuccess);
        }

        [Test]
        public async Task CreateUser_ValidUser_ShouldReturn_OkObjectResult()
        {
            // Arrange
            var usersController = new UsersController(_userServiceMock);
            var requestStub = GetUserRequestStub();
            _userServiceMock
                .GetUsers(Arg.Any<Func<User, bool>>())
                .Returns(new List<User>());

            // Act
            var result = await usersController.CreateUser(requestStub);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task CreateUser_ValidUser_ShouldReturn_UserCreatedResult()
        {
            // Arrange
            var usersController = new UsersController(_userServiceMock);
            var requestStub = GetUserRequestStub();
            _userServiceMock
                .GetUsers(Arg.Any<Func<User, bool>>())
                .Returns(new List<User>());

            // Act
            var result = await usersController.CreateUser(requestStub);

            // Assert
            var objectResult = (OkObjectResult)result;
            var responseResult = (Result)objectResult.Value;
            Assert.IsNull(responseResult.Errors);
            Assert.IsTrue(responseResult.IsSuccess);
        }

        [Test]
        public async Task CreateUser_ValidUser_ShouldCall_CreateUserAsync_FromUserService()
        {
            // Arrange
            var usersController = new UsersController(_userServiceMock);
            var requestStub = GetUserRequestStub();
            _userServiceMock
                .GetUsers(Arg.Any<Func<User, bool>>())
                .Returns(new List<User>());

            // Act
            await usersController.CreateUser(requestStub);

            // Assert
            await _userServiceMock.Received().CreateUserAsync(Arg.Any<User>());
        }

        private CreateUserRequest GetUserRequestStub()
        {
            return new CreateUserRequest
            {
                Address = "Fake street 123",
                Email = "john.doe@email.com",
                Money = 150,
                Name = "John Doe",
                Phone = "+1 57 466321256",
                UserType = "Normal"
            };
        }
    }
}
