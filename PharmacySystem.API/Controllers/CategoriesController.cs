using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.Application.DTOs.Category;
using PharmacySystem.Application.Interfaces.Repositories;
using PharmacySystem.Domain.Entities;
using PharmacySystem.Infrastructure.Repositories;

namespace PharmacySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{AppRole.Admin},{AppRole.Storekeeper},{AppRole.Pharmacist}")]
    public class CategoriesController (IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpPost("Add"),Authorize(Roles =AppRole.Admin)]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var category = _mapper.Map<Category>(dto);
                _unitOfWork.Catigroies.Add(category);
                await _unitOfWork.CompleteAsync();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
