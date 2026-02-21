namespace Jwt_service.Domain;

public record InternalAccessTokenInfo(string subject);

public record AccessTokenInfo(string audience, string subject);
