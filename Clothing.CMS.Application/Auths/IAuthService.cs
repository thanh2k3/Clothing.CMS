using Clothing.CMS.Application.Auths.Dto;
using Clothing.CMS.Application.Common.Dto;

namespace Clothing.CMS.Application.Auths
{
    public interface IAuthService
    {
        //Task<Boolean> LoginAsync(LoginDto model);
        Task<BaseResponse<bool>> LoginAsync(LoginDto model);
        Task LogoutAsync();
    }
}
