using Clothing.CMS.Application.Users.Dto;

namespace Clothing.CMS.Application.Users
{
    public interface IUserService
    {
        Task<ICollection<UserDto>> GetAll();
		Task<EditUserDto> GetById(int id);
        Task<bool> CreateAsync(CreateUserDto model);
		Task<bool> UpdateAsync(EditUserDto model);
        Task<bool> DeleteAsync(int id);
	}
}
