using AutoMapper;
using PharmacySystem.Application.DTOs.Medicine;
using PharmacySystem.Application.Interfaces.Repositories;
using PharmacySystem.Application.Interfaces.Services;
using PharmacySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Infrastructure.Services
{
    public class MedicineService(IUnitOfWork unitOfWork, IMapper mapper, IMedicineRepository medicineRepository) : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository = medicineRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<MedicineFullDto> AddMedicineAsync(AddMedicineDto dto)
        {
            var existingNames = await _medicineRepository.GetAllNamesAsync();
            if (existingNames.Contains(dto.Name, StringComparer.OrdinalIgnoreCase))
                throw new Exception("Medicine already exists");

            var medicine = _mapper.Map<Medicine>(dto);
            _unitOfWork.Medicines.Add(medicine);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<MedicineFullDto>(medicine);
        }

        public async Task<bool> DeleteMedicineAsync(int id)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id);
            if (medicine == null) return false;
            _medicineRepository.Delete(medicine);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<object> GetAllMedicinesAsync(string userRole)
        {
            var medicines = await _medicineRepository.GetAllAsync();
            var result = _mapper.Map<List<MedicineFullDto>>(medicines);
            return userRole switch
            {
                AppRole.Admin => _mapper.Map<List<MedicineFullDto>>(medicines),
                AppRole.Pharmacist => _mapper.Map<List<MedicineLimitedPharmacistDto>>(medicines),
                _ => _mapper.Map<List<MedicineLimitedStorekeeperDto>>(medicines)
            };
        }

        public async Task<object> GetMedicineAsync(int id, string userRole)
        {
            var medicine = await _unitOfWork.Medicines.GetByIdAsync(id);
            if (medicine == null) return null;
            return userRole switch
            {
                AppRole.Admin => _mapper.Map<MedicineFullDto>(medicine),
                AppRole.Pharmacist => _mapper.Map<MedicineLimitedPharmacistDto>(medicine),
                _ => _mapper.Map<MedicineLimitedStorekeeperDto>(medicine)
            };
        }

        public async Task<UpdateMedicineDto> UpdateMedicineAsync(int id, UpdateMedicineDto dto)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id);
            if (medicine == null)
                return null;
            medicine.ExpiryDate = dto.ExpiryDate;
            medicine.CostPrice  = dto.CostPrice;
            medicine.SellingPrice = dto.SellingPrice;
            medicine.Name = dto.Name;
            medicine.GenericName = dto.GenericName;
            medicine.CategoryId = dto.CategoryId;
            medicine.IsDiscontinued = dto.IsDiscontinued;
            medicine.StockQuantity = dto.StockQuantity;

            _medicineRepository.Update(medicine);
            await _unitOfWork.CompleteAsync();
            return dto;
        }
    }
}
