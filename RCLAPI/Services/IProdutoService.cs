using RCLComum.Models; 

namespace RCLAPI.Services {
    public interface IProdutoService {
        Task<bool> CriarProdutoAsync(ProdutoCreateDTO novoProduto);
        Task<IEnumerable<ProdutoDTO>> GetProdutosAtivos();
        Task<ProdutoDTO?> GetProdutoById(int id);
        Task<ProdutoDTO?> GetProdutoRandom();
        Task<IEnumerable<ProdutoDTO>> GetMeusProdutosAsync();
        Task<bool> AtualizarProdutoAsync(int id, ProdutoCreateDTO produtoEditado);
        Task<bool> EliminarProdutoAsync(int id);
    }
}
