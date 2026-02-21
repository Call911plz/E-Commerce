using Jwt_service.Application;
using Jwt_service.Domain;

namespace Jwt_service.Infrastructure;

public class AccessTokenService : IAccessTokenService
{
    public string SignToken(AccessTokenInfo info)
    {
        return string.Empty;
    }
}
