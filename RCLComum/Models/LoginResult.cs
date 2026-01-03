namespace RCLComum.Models {
    public class LoginResult {
        public bool Sucedido { get; set; } 
        public string? Token { get; set; }
        public string? Erro { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
    }
}