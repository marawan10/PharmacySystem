using PharmacySystem.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacySystem.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string id);
        Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync(CreateUserDto dto);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(UpdateUserDto dto);
        Task<(bool Succeeded, string Message, IEnumerable<string> Errors)> DeleteUserAsync(string id);

    }
}
