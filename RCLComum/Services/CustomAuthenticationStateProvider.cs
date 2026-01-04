using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt; 
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace RCLComum.Services {
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(HttpClient httpClient, IJSRuntime jsRuntime) {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
            try {
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

                if (string.IsNullOrWhiteSpace(token))
                    return new AuthenticationState(_anonymous);

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt", "unique_name", "role");
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            catch (InvalidOperationException) {
                return new AuthenticationState(_anonymous);
            }
        }

        public void NotifyUserAuthentication(string token) {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt", "unique_name", "role");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout() {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt) {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.Claims;
        }
    }
}