namespace MyMedia.API.DTOs {
    public class ProdutoDTO {
        public int ProdutoId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal PrecoBase { get; set; } 
        public decimal PrecoFinal { get; set; }
        public int Stock { get; set; }
        public string CategoriaNome { get; set; } = string.Empty;
        public string FornecedorNome { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string ModoNome { get; set; } = string.Empty;
        public bool TemVendas { get; set; }
    }
}