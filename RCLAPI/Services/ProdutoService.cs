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


        // Método auxiliar para garantir que o token é enviado
        private async Task AdicionarToken()
        {
            var token = await _authService.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
        public async Task<IEnumerable<ProdutoDTO>> GetMeusProdutosAsync()
        {
            await AdicionarToken();
            var resultado = await _http.GetFromJsonAsync<List<ProdutoDTO>>("api/Produtos/meus-produtos");
            return resultado ?? new List<ProdutoDTO>();
        }

        public async Task<bool> AtualizarProdutoAsync(int id, ProdutoCreateDTO produtoEditado)
        {
            await AdicionarToken();
            var response = await _http.PutAsJsonAsync($"api/Produtos/{id}", produtoEditado);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarProdutoAsync(int id)
        {
            await AdicionarToken();
            var response = await _http.DeleteAsync($"api/Produtos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}