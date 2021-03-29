using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models.Request;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Strategies;

namespace Sat.Recruitment.Test.Integration
{
    [TestFixture]
    public class UsersControllerIntegrationTest
    {
        private static readonly string usersTestFilePath = Directory.GetCurrentDirectory() + "/Integration/UsersTest.txt";
        private static UsersController _usersController;

        [SetUp]
        public async Task InitializeAsync()
        {
            await InitiliazeUsersFileAsync();
            var csvRepositoryService = new CsvRepositoryService(usersTestFilePath, CultureInfo.InvariantCulture, false);
            var normalUserStrategy = new NormalUserStrategy();
            var superUserStrategy = new SuperUserStrategy();
            var premiumUserStrategy = new PremiuUserStrategy();
            var userService = new UserService(normalUserStrategy, premiumUserStrategy, superUserStrategy, csvRepositoryService);

            _usersController = new UsersController(userService);
        }

        [Test]
        public async Task CreateUser_ValidUser_ShouldCreateUser()
        {
            // Arrange
            var createUserRequestStub = GetCreateUserRequestStub("Mike", "mi.ke+company@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124);

            // Act
            var result = await _usersController.CreateUser(createUserRequestStub);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            await AssertUserCreated("Mike,mike@gmail.com,+349 1122354215,Av. Juan G,Normal,138.88");
        }

        [Test]
        public async Task CreateUser_DuplicatedUser_ShouldReturnBadRequest()
        {
            // Arrange
            var createUserRequestStub = GetCreateUserRequestStub("Agustina", "Agustina@gmail.com", "Garay y Otra Calle", "+534645213542", "SuperUser", 112234);

            // Act
            var result = await _usersController.CreateUser(createUserRequestStub);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        private CreateUserRequest GetCreateUserRequestStub(string name, string email, string address, string phone, string userType, decimal money)
        {
            return new CreateUserRequest
            {
                Name = name,
                Email = email,
                Address = address,
                Money = money,
                Phone = phone,
                UserType = userType
            };
        }

        private async Task InitiliazeUsersFileAsync()
        {
            var usersInformation = new List<string>
            {
                "Juan,Juan@marmol.com,+5491154762312,Peru 2464,Normal,1234",
                "Franco,Franco.Perez@gmail.com,+534645213542,Alvear y Colombres,Premium,112234",
                "Agustina,Agustina@gmail.com,+534645213542,Garay y Otra Calle,SuperUser,112234\n"
            };

            await File.WriteAllLinesAsync(usersTestFilePath, usersInformation);
        }

        private async Task AssertUserCreated(string expectedLine)
        {
            var lines = (await File.ReadAllLinesAsync(usersTestFilePath)).ToList();

            Assert.IsTrue(lines.Any(line => line == expectedLine));
        }
    }
}
