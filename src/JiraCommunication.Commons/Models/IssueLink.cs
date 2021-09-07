
namespace JiraCommunication.Commons.Models
{
    /// <summary>
    /// Class representing a Jira issue link
    /// </summary>
    public class IssueLink
    {
        public string Id { get; set; }
        public LinkType Type { get; set; }
        public Issue OutwardIssue { get; set; }
        public Issue InwardIssue { get; set; }
    }
}
