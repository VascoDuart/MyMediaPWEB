using System.ComponentModel.DataAnnotations;

namespace RCLComum.Models {
    public class RegisterDTO {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A password é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A password deve ter pelo menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirme a sua password")]
        [Compare("Password", ErrorMessage = "As passwords não coincidem")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione o tipo de perfil")]
        public string Role { get; set; } = "Cliente"; 
    }
}