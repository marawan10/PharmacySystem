using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.Application.DTOs;
using PharmacySystem.Application.Interfaces.Services;
using PharmacySystem.Domain.Entities;


namespace PharmacySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<ApplicationUser> userManager, IAuthService authservice, IMapper mapper) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IAuthService _authservice = authservice;
        private readonly IMapper _mapper = mapper;


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.username);
            if (user == null)
                return Unauthorized("Invalid Credentials");
            var passwordValid = await _userManager.CheckPasswordAsync(user, dto.password);
            if (!passwordValid)
                return Unauthorized("Invalid credentials");
            var roles = await _userManager.GetRolesAsync(user);
            var token = await _authservice.GenerateJwtToken(
            user.Id,
            user.Email,
            roles
            );
            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.UserName,
                    user.firstname,
                    user.lastname,
                    roles
                }
            });
        }

       

    }
}
