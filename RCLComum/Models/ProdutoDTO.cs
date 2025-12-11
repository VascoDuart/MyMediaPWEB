namespace RCLComum.Models
{
    // Define o estado do produto (Pendente, Ativo, Inativo)
    public enum EstadoProduto { Pendente, Ativo, Inativo }

    public class ProdutoDTO     //Produto Data Transfer Object que virão da API (RCLApi) para a RCLComum
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal PrecoBase { get; set; } // Preço definido pelo Fornecedor [cite: 264, 387]
        public decimal PrecoFinal { get; set; } // Preço de venda ao Cliente [cite: 263, 264]
        public EstadoProduto Estado { get; set; } // Pendente, Ativo, Inativo [cite: 264, 389]
        public string ImagemUrl { get; set; } = string.Empty;
        public bool ParaVenda { get; set; } // Se é para venda ou apenas listagem [cite: 257, 347]
        public string Categoria { get; set; } = string.Empty;
    }
}