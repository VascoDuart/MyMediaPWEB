using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using RCLComum.Models;

namespace RCLComum.Services {
    public class AuthService {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationStateProvider _authStateProvider;
        private const string BaseApiUrl = "api/Auth"; 

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime, AuthenticationStateProvider authStateProvider) {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _authStateProvider = authStateProvider;
        }

        public async Task<string?> GetToken() {
            try {
                return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            }
            catch {
                return null;
            }
        }

        public async Task<LoginResult> RegisterAsync(RegisterDTO registerDto) {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerDto);

            if (response.IsSuccessStatusCode) {
                return new LoginResult { Sucedido = true };
            }

            var error = await response.Content.ReadAsStringAsync();
            return new LoginResult { Sucedido = false, Erro = "Erro no registo. Verifique os dados." };
        }

        public async Task<LoginResult> LoginAsync(LoginDTO loginDto) {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<LoginResult>();
                if (result != null && !string.IsNullOrEmpty(result.Token)) {
                    result.Sucedido = true; 

                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);
                    ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);
                    return result;
                }
            }

            var errorData = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            return new LoginResult {
                Sucedido = false,
                Erro = errorData?.ContainsKey("Message") == true ? errorData["Message"] : "Falha no login."
            };
        }

        public async Task LogoutAsync() {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserLogout();
        }
    }
}