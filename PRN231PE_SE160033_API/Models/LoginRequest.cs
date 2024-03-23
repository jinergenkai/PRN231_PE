using System.ComponentModel.DataAnnotations;

namespace PRN231PE_SE160033_API.Models
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
