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
        private ClaimsPrincipal _currentClaimsPrincipal;
        private readonly IJSRuntime _jsRuntime;

        public SimpleAuthProvider(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task Login(string userName, string password)
        {
            HttpResponseMessage response =
                await _httpClient.PostAsJsonAsync("auth/login",
                    new LoginRequestDto()
                        { UserName = userName, Password = password });

            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Invalid login attempt.");
            }


            UserDto userDto = JsonSerializer.Deserialize<UserDto>(content,
                new JsonSerializerOptions
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


            NotifyAuthenticationStateChanged(
                Task.FromResult(
                    new AuthenticationState(_currentClaimsPrincipal)));
        }

        public override async Task<AuthenticationState>
            GetAuthenticationStateAsync()
        {
            string userAsJson = "";
            try
            {
                userAsJson = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
            }
            catch (InvalidOperationException e)
            {
                return new AuthenticationState(new());
            }

            if (string.IsNullOrEmpty(userAsJson))
            {
                return new AuthenticationState(new());
            }

            UserDto userDto = JsonSerializer.Deserialize<UserDto>(userAsJson)!;
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userDto.UserName),
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            return new AuthenticationState(claimsPrincipal);
        }
        

        public async Task Logout()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
        }
    }
}