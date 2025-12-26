namespace RCLAPI.DTOs {
    public class ProdutoDTO {
        public int ProdutoId { get; set; } 
        public string Titulo { get; set; } 
        public string Descricao { get; set; }
        public decimal PrecoFinal { get; set; }
        public int Stock { get; set; }
        public string CategoriaNome { get; set; }
        public string FornecedorNome { get; set; }
        public string Estado { get; set; } 
    }
}