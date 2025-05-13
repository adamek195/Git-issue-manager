using GitIssueManager.Core.Dtos;

namespace GitIssueManager.Core.Strategies
{
    /// <summary>
    /// Strategy for GitLabService
    /// </summary>
    public class GitLabStrategy : IServiceStrategy
    {
        private readonly HttpClient _http;

        public GitLabStrategy(HttpClient http)
        {
            _http = http;
        }

        public bool Supports(GitHostingServiceType gitHostingServiceType) => gitHostingServiceType == GitHostingServiceType.GitLab;

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
