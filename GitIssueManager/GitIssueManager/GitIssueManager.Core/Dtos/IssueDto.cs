namespace GitIssueManager.Core.Dtos
{
    public class IssueDto
    {
        /// <summary>
        /// Project Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Issue number
        /// </summary>
        public int IssueNumber { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
    }
}
