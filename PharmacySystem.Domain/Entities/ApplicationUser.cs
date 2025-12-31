using Microsoft.AspNetCore.Identity;
namespace PharmacySystem.Domain.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
}
