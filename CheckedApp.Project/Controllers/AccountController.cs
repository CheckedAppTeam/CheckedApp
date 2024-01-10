using Microsoft.AspNetCore.Mvc;


namespace CheckedAppProject.API.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public ActionResult RegisterUser([FromBody] RegisterUserDTO dto)
        {

        }
    }
}
