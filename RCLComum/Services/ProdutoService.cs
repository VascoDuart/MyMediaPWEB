using RCLComum.Models;
using System.Net.Http.Json;

namespace RCLComum.Services
{
    public class ProdutoService
    {
        // O HttpClient é injetado.
        private readonly HttpClient _httpClient;

        // Base URI da API RestFull. ATENÇÃO: Ajuste esta URL.
        // Para MAUI, use o endereço do Dev Tunnel. Para Web, use o endereço do servidor.
        private const string BaseApiUrl = "https://localhost:7000/api";

        public ProdutoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Funcionalidade: Listar produtos ativos para o utilizador Anónimo/Cliente
        public async Task<List<ProdutoDTO>?> GetProdutosAtivosAsync()
        {
            var url = $"{BaseApiUrl}/produtos/ativos";

            try
            {
                var produtos = await _httpClient.GetFromJsonAsync<List<ProdutoDTO>>(url);
                return produtos ?? new List<ProdutoDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao comunicar com a API ({url}): {ex.Message}");
                return new List<ProdutoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return new List<ProdutoDTO>();
            }
        }
    }
}