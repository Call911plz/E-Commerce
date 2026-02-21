using Jwt_service.Domain;

namespace Jwt_service.Application;

public interface IAccessTokenService
{
    public string SignToken(AccessTokenInfo info);
}
