using Clothing.CMS.Application.Users.Dto;

namespace Clothing.CMS.Application.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<bool> CreateAsync(CreateUserDto model);

        //Task<bool> Create(CreateUserDto model, IFormFile? image);
        //Task<UserDto> GetById(int id);
        //Task Update(EditUserDto model, IFormFile? image);
        //Task Delete(int id);
    }
}
