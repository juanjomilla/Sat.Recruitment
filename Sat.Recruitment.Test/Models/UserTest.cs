using NUnit.Framework;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Request;

namespace Sat.Recruitment.Test.Models
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void CreateNewUser_FromCreateUserRequest_ShouldNormalizeEmailWithDotCharacter()
        {
            // Arrange
            var userRequest = new CreateUserRequest
            {
                Email = "john.doe@email.com"
            };
            var expectedNormalizedEmail = "johndoe@email.com";

            // Act
            var newUser = new User(userRequest);

            // Assert
            Assert.AreEqual(expectedNormalizedEmail, newUser.Email);
        }

        [Test]
        public void CreateNewUser_FromCreateUserRequest_ShouldNormalizeEmailWithPlusCharacter()
        {
            // Arrange
            var userRequest = new CreateUserRequest
            {
                Email = "john+doe@email.com"
            };
            var expectedNormalizedEmail = "john@email.com";

            // Act
            var newUser = new User(userRequest);

            // Assert
            Assert.AreEqual(expectedNormalizedEmail, newUser.Email);
        }

        [Test]
        public void CreateNewUser_FromCreateUserRequest_ShouldNormalizeEmailWithPlusAndDotCharacters()
        {
            // Arrange
            var userRequest = new CreateUserRequest
            {
                Email = "john.doe+company@email.com"
            };
            var expectedNormalizedEmail = "johndoe@email.com";

            // Act
            var newUser = new User(userRequest);

            // Assert
            Assert.AreEqual(expectedNormalizedEmail, newUser.Email);
        }
    }
}
