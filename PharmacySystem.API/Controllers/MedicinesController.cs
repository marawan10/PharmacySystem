using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.Application.DTOs.Medicine;
using PharmacySystem.Application.Interfaces.Services;
using PharmacySystem.Domain.Entities;

namespace PharmacySystem.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = $"{AppRole.Admin},{AppRole.Storekeeper},{AppRole.Pharmacist}")]
    [ApiController]
    public class MedicinesController(IMedicineService medicineService) : ControllerBase
    {

        private readonly IMedicineService _medicineService = medicineService;


        [HttpPost("Add"), Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> AddMedicine([FromBody] AddMedicineDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _medicineService.AddMedicineAsync(dto);
                return CreatedAtAction(nameof(GetMedicine), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMedicine(int id)
        {
            var result = await _medicineService.GetMedicineAsync(id, User.IsInRole(AppRole.Admin) ? AppRole.Admin : User.IsInRole(AppRole.Pharmacist) ? AppRole.Pharmacist : AppRole.Storekeeper);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedicines()
        {
            try
            {
                var result = await _medicineService.GetAllMedicinesAsync(User.IsInRole(AppRole.Admin) ? AppRole.Admin : User.IsInRole(AppRole.Pharmacist) ? AppRole.Pharmacist : AppRole.Storekeeper);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}"), Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> UpdateMedicine(int id, [FromBody] UpdateMedicineDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _medicineService.UpdateMedicineAsync(id, dto);
                if (result == null)
                    return NotFound(new { message = "Medicine not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}"), Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            try
            {
                var success = await _medicineService.DeleteMedicineAsync(id);
                if (!success)
                    return NotFound(new { message = "Medicine not found" });

                return Ok(new { message = "Medicine deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
