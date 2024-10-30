using ApiContracts.Dto_User;
using System.Text.Json;


namespace BlazorApp.Services.UserService
{
    public class HttpUserService : IUserService
    {
        private readonly HttpClient _client;


        public HttpUserService(HttpClient client)
        {
            _client = client;
        }


        public async Task<UserDto> CreateUserAsync(CreateUserDto request)
        {
            HttpResponseMessage httpResponse =
                await _client.PostAsJsonAsync("users", request);
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to create user: {response}");
            }

            return JsonSerializer.Deserialize<UserDto>(response,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }

        public async Task<UserDto> UpdateUserAsync(int id,
            UpdateUserDto request)
        {
            HttpResponseMessage httpResponse =
                await _client.PutAsJsonAsync($"users/{id}", request);
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            return JsonSerializer.Deserialize<UserDto>(response,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }

        public async Task<UserDto> GetSingleUserAsync(int userId)
        {
            HttpResponseMessage httpResponse = await _client.GetAsync($"users/{userId}");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            var user = JsonSerializer.Deserialize<UserDto>(response,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            return user;
        }



        public async Task<List<UserDto>> GetUserAsync()
        {
            HttpResponseMessage httpResponse =
                await _client.GetAsync($"/users");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            return JsonSerializer.Deserialize<List<UserDto>>(response,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }

        public async Task DeleteUserAsync(int userId)
        {
            HttpResponseMessage httpResponse =
                await _client.DeleteAsync($"users/{userId}");
            if (!httpResponse.IsSuccessStatusCode)
            {
                string response =
                    await httpResponse.Content.ReadAsStringAsync();
                throw new Exception(response);
            }
        }
    }
}