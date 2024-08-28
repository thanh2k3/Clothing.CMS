using Clothing.CMS.Application.Users.Dto;

namespace Clothing.CMS.Application.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<bool> CreateAsync(CreateUserDto model);
		Task<EditUserDto> GetById(int id);
		Task<bool> UpdateAsync(EditUserDto model);
        Task<bool> DeleteAsync(int id);
	}
}
