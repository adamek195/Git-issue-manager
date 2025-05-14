using GitIssueManager.Core.Dtos;
using System.Text.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<IssueDto> CreateAsync(IssueCommandDto dto)
        {
            var project = Uri.EscapeDataString($"{dto.Owner}/{dto.Repo}");
            var json = JsonSerializer.Serialize(new
            {
                title = dto.Title,
                body = dto.Body
            });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _httpClient.PostAsync($"projects/{project}/issues", content);
            resp.EnsureSuccessStatusCode();
            using var doc = await JsonDocument.ParseAsync(await resp.Content.ReadAsStreamAsync());
            var number = doc.RootElement.GetProperty("iid").GetInt32();
            return new IssueDto { IssueNumber = number, Body = dto.Body, Title = dto.Title };
        }

        public async Task<IssueDto> UpdateAsync(int issueNumber, IssueCommandDto dto)
        {
            var project = Uri.EscapeDataString($"{dto.Owner}/{dto.Repo}");
            var json = JsonSerializer.Serialize(new
            {
                title = dto.Title,
                body = dto.Body
            });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _httpClient.PutAsync($"projects/{project}/issues/{issueNumber}", content);
            resp.EnsureSuccessStatusCode();
            return new IssueDto { IssueNumber = issueNumber, Body = dto.Body, Title = dto.Title };
        }

        public async Task CloseAsync(int issueNumber, IssueCommandDto dto)
        {
            var project = Uri.EscapeDataString($"{dto.Owner}/{dto.Repo}");
            var json = JsonSerializer.Serialize(new { state_event = "close" });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PutAsync($"projects/{project}/issues/{issueNumber}", content);
            resp.EnsureSuccessStatusCode();
        }
    }
}
