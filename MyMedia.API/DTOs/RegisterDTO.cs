using System.ComponentModel.DataAnnotations;

namespace MyMedia.API.DTOs {
    public class RegisterDTO {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string NomeCompleto { get; set; }

        [Required]
        public string Role { get; set; }
    }
}