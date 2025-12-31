using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.Application.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        public string Id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; } // "Admin" or "Pharmacist"
    }
}
