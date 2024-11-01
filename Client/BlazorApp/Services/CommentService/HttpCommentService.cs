using ApiContracts.DTO_Comment;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace BlazorApp.Services.CommentService
{
    public class HttpCommentService : ICommentService
    {
        private readonly HttpClient _client;

        public HttpCommentService(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommentDto> GetCommentByIdAsync(int commentId)
        {
            HttpResponseMessage httpResponse =
                await _client.GetAsync($"comments/{commentId}");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            return JsonSerializer.Deserialize<CommentDto>(response,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }
        

        public async Task<CommentDto> CreateCommentAsync(int postId,
            CreateCommentDto dto)
        {
            dto.PostId = postId;
            HttpResponseMessage httpResponse =
                await _client.PostAsJsonAsync(
                    $"/comments", dto);
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            return JsonSerializer.Deserialize<CommentDto>(response,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }


        public async Task<CommentDto> UpdateCommentAsync(int commentId,
            UpdateCommentDto dto)
        {
            HttpResponseMessage httpResponse =
                await _client.PutAsJsonAsync($"comments/{commentId}", dto);
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            return JsonSerializer.Deserialize<CommentDto>(response,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }

    
        
            public async Task<List<CommentDto>> GetCommentsForPostAsync(int postId)
            {
                HttpResponseMessage httpResponse = await _client.GetAsync($"comments?postId={postId}");
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception(response);
                }

                return JsonSerializer.Deserialize<List<CommentDto>>(response,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    })!;
            }


        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            HttpResponseMessage httpResponse =
                await _client.DeleteAsync($"comments/{commentId}");
            if (!httpResponse.IsSuccessStatusCode)
            {
                string response =
                    await httpResponse.Content.ReadAsStringAsync();
                throw new Exception(response);
            }

            return true;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsAsync()
        {
            HttpResponseMessage httpResponse =
                await _client.GetAsync($"comments");
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