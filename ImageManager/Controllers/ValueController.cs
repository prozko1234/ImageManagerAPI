using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ImageManager.EntityFramework.Models;

namespace ImageManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [Authorize]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            var userRole = UserRoles.Admin.ToString();
            return Ok($"Ваша роль: {userRole}");
        }
    }
}