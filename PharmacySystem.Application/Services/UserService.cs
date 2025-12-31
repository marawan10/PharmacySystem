using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PharmacySystem.Application.DTOs;
using PharmacySystem.Application.Interfaces.Services;
using PharmacySystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacySystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var result = new List<UserDto>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Firstname = user.firstname, 
                    Lastname = user.lastname,
                    Type = roles.FirstOrDefault()
                });
            }
            return result;
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Firstname = user.firstname,
                Lastname = user.lastname,
                Type = roles.FirstOrDefault()
            };
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync(CreateUserDto dto)
        {
            var user = _mapper.Map<ApplicationUser>(dto);
            // Ensure username/email consistency if needed, assuming Map handles it or dto has it
            
            var result = await _userManager.CreateAsync(user, dto.password);
            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description));
            }

            if (!string.IsNullOrEmpty(dto.Role))
            {
                await _userManager.AddToRoleAsync(user, dto.Role);
            }

            return (true, null);
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(UpdateUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user == null) return (false, new[] { "User not found" });

            user.firstname = dto.firstname;
            user.lastname = dto.lastname;
            user.Email = dto.email;
            user.UserName = dto.UserName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description));
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(dto.Role) && !string.IsNullOrEmpty(dto.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, dto.Role);
            }

            return (true, null);
        }

        public async Task<(bool Succeeded, string Message, IEnumerable<string> Errors)> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return (false, "المستخدم غير موجود", null);

            var roles = await _userManager.GetRolesAsync(user);
            if (user.Email == "marawanmokhtar10@gmail.com")
            {
                return (false, "لا يمكن حذف حساب مدير النظام الأساسي!", null);
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return (false, "حدث خطأ أثناء الحذف", result.Errors.Select(e => e.Description));
            }

            return (true, "تم حذف المستخدم بنجاح", null);
        }
    }
}
