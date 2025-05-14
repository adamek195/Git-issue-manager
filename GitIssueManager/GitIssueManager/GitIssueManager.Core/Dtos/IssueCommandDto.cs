namespace GitIssueManager.Core.Dtos
{
    public class IssueCommandDto
    {
        /// <summary>
        /// Owner of the repo
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Name of the repo
        /// </summary>
        public string Repo { get; set; }

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
