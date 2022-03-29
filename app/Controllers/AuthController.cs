using System.Threading;
using System.Threading.Tasks;
using app.Contracts;
using app.Contracts.Dtos;
using app.Converters;
using app.Services.Services;
using app.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly UserService userService;

        public AuthController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("/CreateUser")]
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserRequestDto userDto,CancellationToken ct)
        {
            var user = await this.userService.CreateAsync(userDto.User.ToModel(), ct);

            if(user is null)
            {
                return this.Ok(
                    new ErrorResponseDto
                    {
                        Success = false,
                        ErrorCode = ErrorCode.UserAlreadyExists.ToDto(),
                        ErrorMessage = $"User with id {userDto.User.Id} already exist",
                    });
            }

            return this.Ok(
                new UserResposeDto
                {
                    User = user.ToDto(),
                });
        }

        [HttpPost("/RemoveUser")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> RemoveUser([FromBody] RemoveUserRequest removeUserDto, CancellationToken ct)
        {
            var user = await this.userService.DeleteAsync(removeUserDto.RemoveUserDto.Id, ct);

            if (user is null)
            {
                return this.Ok(
                    new ErrorResponseDto
                    {
                        Success = false,
                        ErrorCode = ErrorCode.UserNotFound.ToDto(),
                        ErrorMessage = $"User not found",
                    });
            }
            return this.Ok(
                new UserResposeDto
                {
                    User = user.ToDto(),
                    Message = "User was removed",
                });
        }

        [HttpPost("/SetStatus")]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        public async Task<IActionResult> SetStatus([FromForm] SetStatusForm setStatusForm, CancellationToken ct)
        {
            var user = await this.userService.SetStatusAsync(
                setStatusForm.UserId,
                setStatusForm.Status.ToModel(),
                ct);

            return this.Ok(user.ToDto());
        }
    }
}
