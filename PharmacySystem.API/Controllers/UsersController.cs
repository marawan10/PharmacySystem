using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.Application.DTOs;
using PharmacySystem.Application.Interfaces.Services;
using PharmacySystem.Domain.Entities;
using System.Threading.Tasks;

namespace PharmacySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRole.Admin)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (result == null)
                return NotFound("User not found");
            return Ok(result);
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var (succeeded, errors) = await _userService.CreateUserAsync(dto);
            if (succeeded)
                return Ok("User created successfully");
            
            return BadRequest(errors);
        }

        [HttpPatch("edit-user")]
        public async Task<IActionResult> EditUser([FromBody] UpdateUserDto dto)
        {
            var (succeeded, errors) = await _userService.UpdateUserAsync(dto);
            if (succeeded)
                return Ok(new { message = "تم تعديل البيانات بنجاح" });

            return BadRequest(new { message = "حدث خطأ أثناء التعديل", errors = errors });
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var (succeeded, message, errors) = await _userService.DeleteUserAsync(id);
            if (succeeded)
                return Ok(new { message });

            if (message == "المستخدم غير موجود")
                return NotFound(new { message });

            return BadRequest(new { message, errors });
        }
    }
}
