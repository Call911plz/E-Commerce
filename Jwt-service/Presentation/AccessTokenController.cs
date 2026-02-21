using Jwt_service.Application;
using Jwt_service.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Jwt_service.Presentation;

[ApiController]
[Route("auth")]
public class AccessTokenController(IAccessTokenService accessTokenService) : ControllerBase
{
    private readonly IAccessTokenService _accessTokenService = accessTokenService;

    [HttpGet("access")]
    public ActionResult<string> SignAccessToken(AccessTokenInfo info)
    {
        string signedToken = _accessTokenService.SignToken(info);

        return Ok(signedToken);
    }
}
