using app.Helpers;
using app.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public sealed class PublicController : ControllerBase
    {
        private readonly UserService userService;

        public PublicController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("/UserInfo")]
        [Produces("text/plain")]
        public IActionResult UserInfo([FromQuery] int id)
        {
            var user = this.userService.Get(id);
            if (user is null)
                return this.NotFound();
            return this.Content(user.ToHtml(), "text/plain");
        }
    }
}
