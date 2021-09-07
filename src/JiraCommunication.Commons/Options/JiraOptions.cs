namespace JiraCommunication.Commons.Options
{
    /// <summary>
    /// Jira connect options
    /// </summary>
    public class JiraOptions
    {
        /// <summary>
        /// Jira connection Url
        /// </summary>
        public string ServerUrl { get; set; }
        /// <summary>
        /// Jira user name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Jira user password
        /// </summary>
        public string Password { get; set; }
    }
}
