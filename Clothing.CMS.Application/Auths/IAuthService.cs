using Clothing.CMS.Application.Auths.Dto;

namespace Clothing.CMS.Application.Auths
{
    public interface IAuthService
    {
        Task<Boolean> LoginAsync(LoginDto model);
        Task LogoutAsync();
    }
}
