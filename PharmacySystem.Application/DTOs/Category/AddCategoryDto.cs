using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Application.DTOs.Category
{
    public class AddCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; } // لمن قام بالإضافة
        public string? Description { get; set; }

    }
}
