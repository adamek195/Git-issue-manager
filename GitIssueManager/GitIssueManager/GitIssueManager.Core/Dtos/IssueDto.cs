namespace GitIssueManager.Core.Dtos
{
    public class IssueDto
    {
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
        public string Body { get; set; }
    }
}
