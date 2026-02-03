namespace UserMicroService.Application;

public interface ILoginRegisterService
{
    // TODO: Returning string temporarily. Need to return ID JWT
    public Task<string?> LoginUserAsync(UserLoginCredentialDto credentials);
    public Task<string?> RegisterUserAsync(UserRegisterCredentialDto credentials);
}
