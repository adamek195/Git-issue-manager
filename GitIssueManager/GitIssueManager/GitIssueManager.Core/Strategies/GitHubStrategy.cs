using GitIssueManager.Core.Dtos;
using System.Text;
using System.Text.Json;

namespace GitIssueManager.Core.Strategies
{
    public class GitHubStrategy : IServiceStrategy
    {
        private readonly HttpClient _httpClient;

        public GitHubStrategy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool Supports(GitHostingServiceType gitHostingServiceType) => gitHostingServiceType == GitHostingServiceType.GitHub;

        public async Task<IssueDto> CreateAsync(IssueCommandDto dto, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(new
            {
                title = dto.Title,
                body = dto.Body
            });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"repos/{dto.Owner}/{dto.Repo}/issues", content, cancellationToken);
            response.EnsureSuccessStatusCode();

            using var doc = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync(cancellationToken));
            var number = doc.RootElement.GetProperty("number").GetInt32();
            return new IssueDto { IssueNumber = number, Body = dto.Body , Title = dto.Title };
        }

        public async Task<IssueDto> UpdateAsync(int issueNumber, IssueCommandDto dto, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(new
            {
                title = dto.Title,
                body = dto.Body
            });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PatchAsync($"repos/{dto.Owner}/{dto.Repo}/issues/{issueNumber}", content, cancellationToken);
            resp.EnsureSuccessStatusCode();
            return new IssueDto { IssueNumber = issueNumber, Body = dto.Body, Title = dto.Title }; ;
        }

        public async Task CloseAsync(int issueNumber, StateIssueDto dto, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(new { state = "closed" });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PatchAsync($"repos/{dto.Owner}/{dto.Repo}/issues/{issueNumber}", content, cancellationToken);
            resp.EnsureSuccessStatusCode();
        }
    }
}
