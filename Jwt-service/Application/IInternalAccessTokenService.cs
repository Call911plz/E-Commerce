using Jwt_service.Domain;

namespace Jwt_service.Application;

public interface IInternalAccessTokenService
{
    public string SignToken(InternalAccessTokenInfo info);
}
