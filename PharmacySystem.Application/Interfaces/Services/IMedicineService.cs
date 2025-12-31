using AutoMapper;
using PharmacySystem.Application.DTOs.Medicine;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Application.Interfaces.Services
{
    public interface IMedicineService
    {
        Task<MedicineFullDto> AddMedicineAsync(AddMedicineDto dto);
        Task<object> GetMedicineAsync(int id, string userRole);
        Task<object> GetAllMedicinesAsync(string userRole);
        Task<UpdateMedicineDto> UpdateMedicineAsync(int id, UpdateMedicineDto dto);
        Task<bool> DeleteMedicineAsync(int id);
    }
}
