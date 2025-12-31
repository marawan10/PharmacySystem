
using AutoMapper;
using PharmacySystem.Application.DTOs;
using PharmacySystem.Application.DTOs.Category;
using PharmacySystem.Application.DTOs.Medicine;
using PharmacySystem.Domain.Entities;

namespace PharmacySystem.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            // User
            CreateMap<ApplicationUser, CreateUserDto>();
            CreateMap<CreateUserDto, ApplicationUser>();

            CreateMap<GetUserDto, ApplicationUser>();
            CreateMap<ApplicationUser, GetUserDto>();


            // Medicines
            CreateMap<AddMedicineDto, Medicine>();
            CreateMap<Medicine, AddMedicineDto>();

            CreateMap<MedicineFullDto, Medicine>();
            CreateMap<Medicine, MedicineFullDto>();

            CreateMap<Medicine, UpdateMedicineDto>();
            CreateMap<UpdateMedicineDto, Medicine>();


            CreateMap<MedicineLimitedStorekeeperDto, Medicine>();
            CreateMap<Medicine, MedicineLimitedStorekeeperDto>();

            CreateMap<MedicineLimitedPharmacistDto, Medicine>();
            CreateMap<Medicine, MedicineLimitedPharmacistDto>();


            // Category
            CreateMap<Category, AddCategoryDto>();
            CreateMap<AddCategoryDto, Category>();
        }
    }
}
