using System.Security.Claims;
using System.Text.Json;
using ApiContracts.Dto_LoginRequest;
using ApiContracts.Dto_User;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace BlazorApp.Auth
{
    public class SimpleAuthProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private ClaimsPrincipal _currentClaimsPrincipal = new(new ClaimsIdentity());
        private bool _hasCheckedSessionStorage = false;

        public SimpleAuthProvider(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task Login(string userName, string password)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
                "auth/login",
                new LoginRequestDto { UserName = userName, Password = password });

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Invalid login attempt.");
            }

            string content = await response.Content.ReadAsStringAsync();
            UserDto userDto = JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userDto.UserName),
                new Claim("Id", userDto.Id.ToString())
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
            _currentClaimsPrincipal = new ClaimsPrincipal(identity);

            // Persist the user state in sessionStorage
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", JsonSerializer.Serialize(userDto));

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentClaimsPrincipal)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!_hasCheckedSessionStorage)
            {
                _hasCheckedSessionStorage = true;
                await RestoreUserFromSessionStorage();
            }

            return new AuthenticationState(_currentClaimsPrincipal);
        }

        private async Task RestoreUserFromSessionStorage()
        {
            try
            {
                var userAsJson = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    UserDto userDto = JsonSerializer.Deserialize<UserDto>(userAsJson)!;
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userDto.UserName),
                        new Claim("Id", userDto.Id.ToString())
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
                    _currentClaimsPrincipal = new ClaimsPrincipal(identity);
                }
            }
            catch (InvalidOperationException)
            {
                // JS interop failed during prerendering; ignore and try later in the component lifecycle
            }
        }

        public async Task Logout()
        {
            _currentClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "currentUser");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentClaimsPrincipal)));
        }
    }
}
