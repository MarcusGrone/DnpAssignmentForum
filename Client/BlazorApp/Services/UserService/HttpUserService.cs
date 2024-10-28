using ApiContracts.Dto_User;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System;

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
                throw new Exception(response);
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
            HttpResponseMessage httpResponse =
                await _client.GetAsync($"users/{userId}");
            string response =
                await httpResponse.Content.ReadAsStringAsync();
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



        public async Task<IEnumerable<UserDto>> GetUserAsync()
        {
            HttpResponseMessage httpResponse =
                await _client.GetAsync($"/users");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            return JsonSerializer.Deserialize<IEnumerable<UserDto>>(response,
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