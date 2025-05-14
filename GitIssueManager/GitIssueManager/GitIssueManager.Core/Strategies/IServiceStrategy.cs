using GitIssueManager.Core.Dtos;

namespace GitIssueManager.Core.Strategies
{
    public interface IServiceStrategy
    {
        bool Supports(GitHostingServiceType gitHostingServiceType);

        Task<IssueDto> CreateAsync(IssueCommandDto dto);

        Task<IssueDto> UpdateAsync(int issueNumber, IssueCommandDto dto);

        Task CloseAsync(int issueNumber, StateIssueDto dto);
    }
}
