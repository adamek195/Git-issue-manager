using GitIssueManager.Core.Dtos;

namespace GitIssueManager.Core.Strategies
{
    public interface IServiceStrategy
    {
        bool Supports(GitHostingServiceType gitHostingServiceType);

        Task<IssueDto> CreateAsync(IssueCommandDto dto, CancellationToken cancellationToken);

        Task<IssueDto> UpdateAsync(int issueNumber, IssueCommandDto dto, CancellationToken cancellationToken);

        Task CloseAsync(int issueNumber, StateIssueDto dto, CancellationToken cancellationToken);
    }
}
