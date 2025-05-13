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
        Task<IssueDto> CreateAsync(IssueDto dto);

        /// <summary>
        /// Update Issue
        /// </summary>
        /// <param name="dto">Issue dto</param>
        /// <returns></returns>
        Task<IssueDto> UpdateAsync(IssueDto dto);

        /// <summary>
        /// Close Issue
        /// </summary>
        /// <param name="dto">Issue dto</param>
        /// <returns></returns>
        Task CloseAsync(IssueDto dto);
    }
}
