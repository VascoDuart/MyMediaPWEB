using RCLComum.Models; 

namespace RCLAPI.Services {
    public interface IProdutoService {
        Task<bool> CriarProdutoAsync(ProdutoCreateDTO novoProduto);
        Task<IEnumerable<ProdutoDTO>> GetProdutosAtivos();
        Task<ProdutoDTO?> GetProdutoById(int id);
        Task<ProdutoDTO?> GetProdutoRandom();
    }
}
