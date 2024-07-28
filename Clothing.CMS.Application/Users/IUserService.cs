using Clothing.CMS.Application.Common.Dto;
using Clothing.CMS.Application.Users.Dto;
using Microsoft.AspNetCore.Http;

namespace Clothing.CMS.Application.Users
{
    public interface IUserService
    {
        Task<PagedResponseDto<List<UserDto>>> GetAllPaging(UserPagedRequestDto input);
        Task<IEnumerable<UserDto>> GetAll();


        //Task<bool> Create(CreateUserDto model, IFormFile? image);
        //Task<UserDto> GetById(int id);
        //Task Update(EditUserDto model, IFormFile? image);
        //Task Delete(int id);
    }
}
