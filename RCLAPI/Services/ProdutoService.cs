using RCLAPI.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace RCLAPI.Services {
    public class ProdutoService : IProdutoService {
        private readonly HttpClient _http;

        public ProdutoService(HttpClient http) {
            _http = http;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutosAtivos() {
            return await _http.GetFromJsonAsync<IEnumerable<ProdutoDTO>>("api/Produtos");
        }

        public async Task<ProdutoDTO?> GetProdutoById(int id) {
            return await _http.GetFromJsonAsync<ProdutoDTO>($"api/Produtos/{id}");
        }
    }
}