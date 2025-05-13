using GitIssueManager.Core.Dtos;

namespace GitIssueManager.Core.Strategies
{
    /// <summary>
    /// Strategy for GitHubService
    /// </summary>
    public class GitHubStrategy : IServiceStrategy
    {
        private readonly HttpClient _httpClient;

        public GitHubStrategy(HttpClient http)
        {
            _httpClient = http;
        }

        public bool Supports(GitHostingServiceType gitHostingServiceType) => gitHostingServiceType == GitHostingServiceType.GitHub;

        public Task<IssueDto> CreateAsync(IssueDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<IssueDto> UpdateAsync(IssueDto dto)
        {
            throw new NotImplementedException();
        }

        public Task CloseAsync(IssueDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
