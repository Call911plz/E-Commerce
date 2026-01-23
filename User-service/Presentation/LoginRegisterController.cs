using Microsoft.AspNetCore.Mvc;
using UserMicroService.Application;

namespace UserMicroService.Presentation;

[ApiController]
[Route("")]
public class LoginRegisterController(ILoginRegisterService loginService) : ControllerBase
{
    private readonly ILoginRegisterService _loginService = loginService;

    [HttpGet("login")]
    public ActionResult<string> Login(UserLoginCredentialDto credentials)
    {
        return Ok(_loginService.LoginUser(credentials));
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(UserRegisterCredentialDto credentials)
    {
        return Ok(await _loginService.RegisterUserAsync(credentials));
    }
}
