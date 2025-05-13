using GitIssueManager.Core.Dtos;
using System.Text.Json;
using System.Text;

namespace GitIssueManager.Core.Strategies
{
    /// <summary>
    /// Strategy for GitLabService
    /// </summary>
    public class GitLabStrategy : IServiceStrategy
    {
        private readonly HttpClient _httpClient;

        public GitLabStrategy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool Supports(GitHostingServiceType gitHostingServiceType) => gitHostingServiceType == GitHostingServiceType.GitLab;

        public async Task<IssueDto> CreateAsync(IssueDto dto)
        {
            var json = JsonSerializer.Serialize(new
            {
                title = dto.Title,
                body = dto.Body
            });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _httpClient.PostAsync(string.Empty, content);
            resp.EnsureSuccessStatusCode();
            using var doc = await JsonDocument.ParseAsync(await resp.Content.ReadAsStreamAsync());
            dto.IssueNumber = doc.RootElement.GetProperty("iid").GetInt32();
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

            var resp = await _httpClient.PutAsync($"/{dto.IssueNumber}", content);
            resp.EnsureSuccessStatusCode();
            return dto;
        }

        public async Task CloseAsync(IssueDto dto)
        {
            var json = JsonSerializer.Serialize(new { state_event = "close" });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PutAsync($"/{dto.IssueNumber}", content);
            resp.EnsureSuccessStatusCode();
        }
    }
}
