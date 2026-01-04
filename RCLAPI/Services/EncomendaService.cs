using RCLComum.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using RCLComum.Models;
using RCLAPI.Services;

namespace RCLAPI.Services {
    public class EncomendaService : IEncomendaService {
        private readonly HttpClient _http;
        private readonly AuthService _authService;

        public EncomendaService(HttpClient http, AuthService authService) {
            _http = http;
            _authService = authService;
        }

        public async Task<bool> FinalizarEncomendaAsync(EncomendaCreateDTO encomenda) {
            var token = await _authService.GetToken();
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.PostAsJsonAsync("api/Encomendas", encomenda);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<EncomendaListaDTO>> GetEncomendasClienteAsync() {
            try {
                var token = await _authService.GetToken();
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var resultado = await _http.GetFromJsonAsync<List<EncomendaListaDTO>>("api/Encomendas");

                return resultado ?? new List<EncomendaListaDTO>();
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro ao procurar histórico: {ex.Message}");
                return new List<EncomendaListaDTO>();
            }
        }
    }
}
