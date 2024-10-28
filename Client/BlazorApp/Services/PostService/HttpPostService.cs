using ApiContracts.DTO_Comment;
using ApiContracts.DTO_Post;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using ApiContracts.Dto_User;

namespace BlazorApp.Services.PostService
{
    public class HttpPostService : IPostService
    {
        private readonly HttpClient _client;

        public HttpPostService(HttpClient client)
        {
            _client = client;
        }
        
        public async Task<IEnumerable<PostDto>> GetPostsAsync()
        {
            HttpResponseMessage httpResponse =
                await _client.GetAsync($"/posts");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            return JsonSerializer.Deserialize<IEnumerable<PostDto>>(response,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }


        public async Task<PostDto> GetPostByIdAsync(int postId)
        {
            HttpResponseMessage httpResponse =
                await _client.GetAsync($"posts/{postId}");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            return JsonSerializer.Deserialize<PostDto>(response,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }
        
        public async Task<PostDto> CreatePostAsync(CreatePostDto dto)
        {
            HttpResponseMessage httpResponse =
                await _client.PostAsJsonAsync("posts", dto);
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            return JsonSerializer.Deserialize<PostDto>(response,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }
        

        public async Task<PostDto> UpdatePostAsync(int postId,
            UpdatePostDto dto)
        {
            HttpResponseMessage httpResponse =
                await _client.PutAsJsonAsync($"posts/{postId}", dto);
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            return JsonSerializer.Deserialize<PostDto>(response,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }


        public async Task<bool> DeletePostAsync(int postId)
        {
            HttpResponseMessage httpResponse =
                await _client.DeleteAsync($"posts/{postId}");
            if (!httpResponse.IsSuccessStatusCode)
            {
                string response =
                    await httpResponse.Content.ReadAsStringAsync();
                throw new Exception(response);
            }

            return true;
        }

     
        public async Task<IEnumerable<CommentDto>> GetCommentsForPostAsync(
            int postId)
        {
            HttpResponseMessage httpResponse =
                await _client.GetAsync($"posts/{postId}/comments");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            return JsonSerializer.Deserialize<IEnumerable<CommentDto>>(response,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }
    }
}
