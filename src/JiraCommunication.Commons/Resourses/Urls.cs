using System.IO;

namespace JiraCommunication.Commons.Resourses
{
    public class Urls
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string IssueByKey(string issueKey) => Url($"issue/{issueKey}");

        public static string Issue() => Url("issue");

        public static string Search() => Url("search");

        public static string Priority() => Url("priority");

        public static string CreateMeta() => Url("issue/createmeta");

        public static string Status() => Url("status");

        public static string Versions(string projectKey) => Url($"project/{projectKey}/versions");

        public static string Version() => Url("version");

        public static string UpdateVersion(string versionId) => Url($"version/{versionId}");

        public static string ApplicationProperties() => Url("application-properties");

        public static string AttachmentById(string attachmentId) => Url($"attachment/{attachmentId}");

        public static string Project() => Url("project");

        #region Private
        /// <summary>
        /// 
        /// </summary>
        private const string BaseUrl = "/rest/api/2/";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string Url(string key) => Path.Combine(BaseUrl, key);
        #endregion
    }
}
