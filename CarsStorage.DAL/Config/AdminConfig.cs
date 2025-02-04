
using System.ComponentModel.DataAnnotations;

namespace CarsStorage.DAL.Config
{
    public class AdminConfig
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}
