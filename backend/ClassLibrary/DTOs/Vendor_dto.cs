
using System.ComponentModel.DataAnnotations;

namespace final_proj.DTO
{
    public class CreateVendorDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
