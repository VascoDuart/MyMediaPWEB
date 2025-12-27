using RCLAPI.DTOs; 

namespace RCLAPI.Services {
    public interface IProdutoService {
        Task<IEnumerable<ProdutoDTO>> GetProdutosAtivos();
        Task<ProdutoDTO?> GetProdutoById(int id);
    }
}
