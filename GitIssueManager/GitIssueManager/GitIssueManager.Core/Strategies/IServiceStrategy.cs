using GitIssueManager.Core.Dtos;

namespace GitIssueManager.Core.Strategies
{
    /// <summary>
    /// Intefrace for strategy
    /// </summary>
    public interface IServiceStrategy
    {
        /// <summary>
        /// Select strategy
        /// </summary>
        /// <param name="gitHostingServiceType">Hosting service Type</param>
        /// <returns></returns>
        bool Supports(GitHostingServiceType gitHostingServiceType);

        /// <summary>
        /// Create issue
        /// </summary>
        /// <param name="dto">Issue dto</param>
        /// <returns></returns>
        Task<IssueDto> CreateAsync(IssueCommandDto dto);

        /// <summary>
        /// Update Issue
        /// </summary>
        /// <param name="issueNumber">number of issue</param>
        /// <param name="dto">Issue dto</param>
        /// <returns></returns>
        Task<IssueDto> UpdateAsync(int issueNumber, IssueCommandDto dto);

        /// <summary>
        /// Close Issue
        /// </summary>
        /// <param name="issueNumber">number of issue</param>
        /// <returns></returns>
        Task CloseAsync(int issueNumber, IssueCommandDto dto);
    }
}
