using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Request;
using Sat.Recruitment.Api.Models.Response;
using Sat.Recruitment.Api.Services;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequest request)
        {
            try
            {
                var result = new Result();
                var newUser = new User(request);

                var users = _userService
                    .GetUsers(user => user.Email == newUser.Email || user.Phone == newUser.Phone || (user.Address == newUser.Address && user.Name == newUser.Name));

                if (users.Any())
                {
                    result.IsSuccess = false;
                    result.Errors = "The user is duplicated";

                    return new BadRequestObjectResult(result);
                }

                await _userService.CreateUserAsync(newUser);

                result.IsSuccess = true;

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An exception has occurred.\nException message: {0}\nException details: {1}", ex.Message, ex.ToString());

                return new BadRequestObjectResult(string.Format("An error has occurred: {0}", ex.Message));
            }
        }
    }
}
