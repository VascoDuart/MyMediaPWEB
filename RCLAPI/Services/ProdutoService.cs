using RCLComum.Models;
using RCLComum.Services; 
using System.Net.Http.Headers; 
using System.Net.Http.Json;

namespace RCLAPI.Services {
    public class ProdutoService : IProdutoService {
        private readonly HttpClient _http;
        private readonly AuthService _authService; 

        public ProdutoService(HttpClient http, AuthService authService) {
            _http = http;
            _authService = authService;
        }

        public async Task<bool> CriarProdutoAsync(ProdutoCreateDTO novoProduto) {
            var token = await _authService.GetToken();

            if (!string.IsNullOrEmpty(token)) {
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.PostAsJsonAsync("api/Produtos", novoProduto);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutosAtivos() {
            return await _http.GetFromJsonAsync<IEnumerable<ProdutoDTO>>("api/Produtos")
                   ?? new List<ProdutoDTO>();
        }

        public async Task<ProdutoDTO?> GetProdutoById(int id) {
            return await _http.GetFromJsonAsync<ProdutoDTO>($"api/Produtos/{id}");
        }

        public async Task<ProdutoDTO?> GetProdutoRandom()
        {
            try
            {
                return await _http.GetFromJsonAsync<ProdutoDTO>("api/Produtos/random");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}