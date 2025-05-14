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

        public async Task<IssueDto> CreateAsync(IssueCommandDto dto)
        {
            var json = JsonSerializer.Serialize(new
            {
                title = dto.Title,
                body = dto.Body
            });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"repos/{dto.Owner}/{dto.Repo}/issues", content);
            response.EnsureSuccessStatusCode();

            using var doc = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
            var number = doc.RootElement.GetProperty("number").GetInt32();
            return new IssueDto { IssueNumber = number, Body = dto.Body , Title = dto.Title };
        }

        public async Task<IssueDto> UpdateAsync(int issueNumber, IssueCommandDto dto)
        {
            var json = JsonSerializer.Serialize(new
            {
                title = dto.Title,
                body = dto.Body
            });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PatchAsync($"repos/{dto.Owner}/{dto.Repo}/issues/{issueNumber}", content);
            resp.EnsureSuccessStatusCode();
            return new IssueDto { IssueNumber = issueNumber, Body = dto.Body, Title = dto.Title }; ;
        }

        public async Task CloseAsync(int issueNumber, IssueCommandDto dto)
        {
            var json = JsonSerializer.Serialize(new { state = "closed" });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PatchAsync($"repos/{dto.Owner}/{dto.Repo}/issues/{issueNumber}", content);
            resp.EnsureSuccessStatusCode();
        }
    }
}
