using System;

namespace JiraCommunication.Commons.Exceptions
{
    /// <summary>
    /// Custom JiraClient exception
    /// </summary>
    [Serializable()]
    public class JiraClientException : Exception
    {
        public JiraClientException() : base() { }
        public JiraClientException(string message) : base(message) { }
        public JiraClientException(string message, Exception inner) : base(message, inner) { }
    }
}
