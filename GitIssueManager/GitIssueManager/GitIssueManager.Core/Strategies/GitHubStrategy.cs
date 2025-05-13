using GitIssueManager.Core.Dtos;
using System.Text;
using System.Text.Json;

namespace GitIssueManager.Core.Strategies
{
    /// <summary>
    /// Strategy for GitHubService
    /// </summary>
    public class GitHubStrategy : IServiceStrategy
    {
        private readonly HttpClient _httpClient;

        public GitHubStrategy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool Supports(GitHostingServiceType gitHostingServiceType) => gitHostingServiceType == GitHostingServiceType.GitHub;

        public async Task<IssueDto> CreateAsync(IssueDto dto)
        {
            var json = JsonSerializer.Serialize(new
            {
                title = dto.Title,
                body = dto.Body
            });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(string.Empty, content);
            response.EnsureSuccessStatusCode();

            using var doc = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
            dto.IssueNumber = doc.RootElement.GetProperty("number").GetInt32();
            return dto;
        }

        public async Task<IssueDto> UpdateAsync(IssueDto dto)
        {
            var json = JsonSerializer.Serialize(new
            {
                title = dto.Title,
                body = dto.Body
            });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PatchAsync($"/{dto.IssueNumber}", content);
            resp.EnsureSuccessStatusCode();
            return dto;
        }

        public async Task CloseAsync(IssueDto dto)
        {
            var json = JsonSerializer.Serialize(new
            {
                title = dto.Title,
                body = dto.Body
            });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PatchAsync($"/{dto.IssueNumber}", content);
            resp.EnsureSuccessStatusCode();
        }
    }
}
