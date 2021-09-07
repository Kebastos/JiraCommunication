using JiraCommunication.Commons.Constants;
using JiraCommunication.Commons.Exceptions;
using JiraCommunication.Commons.Models;
using JiraCommunication.Commons.Options;
using JiraCommunication.Commons.Resourses;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace JiraCommunication.Services.JiraClient
{
    /// <summary>
    /// The class implements the logic of working with Jira.
    /// http://docs.atlassian.com/jira/REST/latest/ for documentation of the Jira API.
    /// </summary>
    public class JiraClientService : IJiraClientService
    {
        private readonly RestClient _restClient;

        /// <summary>
        /// Create a Jira Client instance
        /// </summary>
        /// <param name="jiraOptions">Jira connection options</param>
        public JiraClientService(JiraOptions jiraOptions)
        {
            _restClient = new RestClient(jiraOptions.ServerUrl)
            {
                Authenticator = new HttpBasicAuthenticator(jiraOptions.UserName, jiraOptions.Password)
            };
        }

        /// <summary>
        /// Create a Jira Client instance with DI
        /// </summary>
        /// <param name="jiraOptions">Jira connection options</param>
        public JiraClientService(IOptionsMonitor<JiraOptions> jiraOptions)
        {
            var option = jiraOptions.CurrentValue;
            _restClient = new RestClient(option.ServerUrl)
            {
                Authenticator = new HttpBasicAuthenticator(option.UserName, option.Password)
            };
        }

        /// <summary>
        /// Executes a request and returns the deserialized response. 
        /// If the response hasn't got the specified expected response code or if an exception was thrown during execution a JiraClientException will be  thrown.
        /// </summary>
        /// <typeparam name="T">Request return type</typeparam>
        /// <param name="request">Request to execute</param>
        /// <param name="expectedResponseCode">The expected HTTP response code</param>
        /// <returns>Deserialized response of request</returns>
        public T Execute<T>(RestRequest request) where T : new()
        {
            // Won't throw exception.
            var response = _restClient.Execute<T>(request);

            ValidateResponse(response);

            return response.Data;
        }

        /// <summary>
        /// Returns the Issue with the specified key. If the fields parameters specified only the given field names will be loaded.  
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="issueKey">Issue key</param>
        /// <param name="fields">Fields to load</param>
        /// <returns>
        /// The issue with the specified key or null if no such issue was found.
        /// </returns>
        public Issue GetIssue(string issueKey, IEnumerable<string> fields = null)
        {
            var request = new RestRequest
            {
                Resource = $"{Urls.IssueByKey(issueKey)}?fields={GetFieldsToString(fields)}",
                Method = Method.GET
            };

            var issue = Execute<Issue>(request);
            return issue.fields != null ? issue : null;
        }

        /// <summary>
        /// Returns True if the change was successful, or False if for some reason it was not possible to update the fields.
        /// </summary>
        /// <param name="issuekey">Issue key</param>
        /// <param name="fields">Fields to load</param>
        /// <returns>True - if the update was successful. False - if there are problems for some reason.</returns>
        public bool UpdateIssueFields(string issuekey, object fields)
        {
            var request = new RestRequest
            {
                Resource = $"{Urls.IssueByKey(issuekey)}",
                Method = Method.PUT,
                RequestFormat = DataFormat.Json,
            };

            request.AddJsonBody(fields);

            var response = _restClient.Execute(request);

            ValidateResponse(response);

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        /// <summary>
		/// Searches for Issues using JQL. 
        /// Throws a JiraClientException if the request was unable to execute.
		/// </summary>
		/// <param name="jql">JQL</param>
		/// <param name="startAt">Start at...</param>
		/// <param name="maxResults">The maximum number of returned results</param>
		/// <param name="fields">Fields to search</param>
		/// <returns>The results of the search performed by the JQL query</returns>
		public Issues GetIssuesByJql(string jql, int startAt, int maxResults, IEnumerable<string> fields = null)
        {
            var request = new RestRequest { Resource = Urls.Search() };
            request.AddParameter(JiraQueryConstants.Jql, jql, ParameterType.GetOrPost);
            request.AddParameter(JiraQueryConstants.Fields, GetFieldsToString(fields), ParameterType.GetOrPost);
            request.AddParameter(JiraQueryConstants.StartAt, startAt, ParameterType.GetOrPost);
            request.AddParameter(JiraQueryConstants.MaxResult, maxResults, ParameterType.GetOrPost);
            request.Method = Method.GET;
            return Execute<Issues>(request);
        }

        /// <summary>
        /// Returns the Issues for the specified project.  
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="projectKey">Project key</param>
        /// <param name="startAt">Start at...</param>
        /// <param name="maxResults">The maximum number of returned results</param>
        /// <param name="fields">Fields to search</param>
        /// <returns>All Issues of the specified project</returns>
        public Issues GetIssuesByProject(string projectKey, int startAt, int maxResults, IEnumerable<string> fields = null)
        {
            return GetIssuesByJql("project=" + projectKey, startAt, maxResults, fields);
        }

        /// <summary>
        /// Returns all available projects the current user has permision to view. 
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <returns>Details of all projects visible to user</returns>
        public List<Project> GetProjects()
        {
            var request = new RestRequest()
            {
                Resource = Urls.Project(),
                RequestFormat = DataFormat.Json,
                Method = Method.GET
            };

            return Execute<List<Project>>(request);
        }

        /// <summary>
        /// Returns a list of all possible priorities.  
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <returns>All possible priorities</returns>
        public List<Priority> GetPriorities()
        {
            var request = new RestRequest();
            request.Resource = Urls.Priority();
            request.Method = Method.GET;
            return Execute<List<Priority>>(request);
        }

        /// <summary>
        /// Returns the meta data for creating issues. 
        /// This includes the available projects and issue types, but not fields (fields are supported in the Jira api but not by this wrapper currently).
        /// </summary>
        /// <param name="projectKey">Project key</param>
        /// <returns>The meta data for creating issues</returns>
        public ProjectMeta GetProjectMeta(string projectKey)
        {
            var request = new RestRequest { Resource = Urls.CreateMeta() };
            request.AddParameter("projectKeys", projectKey, ParameterType.GetOrPost);
            request.Method = Method.GET;
            var createMeta = Execute<IssueCreateMeta>(request);
            if (createMeta.Projects[0].Key != projectKey || createMeta.Projects.Count != 1)
                throw new JiraClientException();
            return createMeta.Projects[0];
        }

        /// <summary>
        /// Returns a list of all possible issue statuses. 
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <returns>All possible issue statuses</returns>
        public List<Status> GetStatuses()
        {
            var request = new RestRequest();
            request.Resource = Urls.Status();
            request.Method = Method.GET;
            return Execute<List<Status>>(request);
        }

        /// <summary>
        /// Creates and returns a new Version.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="newVersion">Parameters of the new Version</param>
        /// <returns>Created Version</returns>
        public Version CreateVersion(NewVersion newVersion)
        {
            var request = new RestRequest()
            {
                Resource = Urls.Version(),
                RequestFormat = DataFormat.Json,
                Method = Method.POST
            };

            request.AddJsonBody(newVersion);

            return Execute<Version>(request);
        }

        /// <summary>
        /// Updates the specified version.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="version">Version parameters to update</param>
        /// <returns>Updated Version</returns>
        public Version UpdateVersion(UpdateVersion version)
        {
            var request = new RestRequest
            {
                Resource = Urls.Version(),
                RequestFormat = DataFormat.Json,
                Method = Method.PUT
            };

            request.AddJsonBody(version);

            return Execute<Version>(request);
        }

        /// <summary>
        /// Returns all versions of the specified project.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="projectKey">Project key</param>
        /// <returns>All versions of the specified project</returns>
        public IEnumerable<Version> GetVersions(string projectKey)
        {
            var request = new RestRequest
            {
                Resource = Urls.Versions(projectKey),
                Method = Method.GET
            };
            return Execute<List<Version>>(request);
        }

        /// <summary>
        /// Creates a new issue.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="newIssue">Parameters of the new issue</param>
        /// <returns>The new issue</returns>
        public BasicIssue CreateIssue(CreateIssue newIssue)
        {
            var request = new RestRequest()
            {
                Resource = Urls.Issue(),
                RequestFormat = DataFormat.Json,
                Method = Method.POST
            };

            request.AddJsonBody(newIssue);

            return Execute<BasicIssue>(request);
        }

        /// <summary>
        /// Returns the application property with the specified key.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="propertyKey">The property key</param>
        /// <returns>The application property with the specified key</returns>
        public ApplicationProperty GetApplicationProperty(string propertyKey)
        {
            var request = new RestRequest()
            {
                Method = Method.GET,
                Resource = Urls.ApplicationProperties(),
                RequestFormat = DataFormat.Json
            };

            request.AddParameter("key", propertyKey, ParameterType.GetOrPost);

            return Execute<ApplicationProperty>(request);
        }

        /// <summary>
        /// Returns the attachment with the specified id.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="attachmentId">Attachment id</param>
        /// <returns>The attachment with the specified id</returns>
        public Attachment GetAttachment(string attachmentId)
        {
            var request = new RestRequest()
            {
                Method = Method.GET,
                Resource = Urls.AttachmentById(attachmentId),
                RequestFormat = DataFormat.Json
            };

            return Execute<Attachment>(request);
        }

        /// <summary>
        /// Deletes the specified attachment.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="attachmentId">Attachment id to delete</param>
        public bool DeleteAttachment(string attachmentId)
        {
            var request = new RestRequest()
            {
                Method = Method.DELETE,
                Resource = Urls.AttachmentById(attachmentId)
            };

            var response = _restClient.Execute(request);
            if (response.ResponseStatus != ResponseStatus.Completed || response.StatusCode != HttpStatusCode.NoContent)
                throw new JiraClientException("Failed to delete attachment with id=" + attachmentId);

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        /// <summary>
        /// Update time tracking estimates
        /// </summary>
        /// <param name="issuekey">Issue key</param>
        /// <param name="orginialEstimateMinutes">Original estimate(minutes)</param>
        /// <param name="remainingEstimateMinutes">Remaining estimate(minutes)</param>
        /// <returns></returns>
        public bool UpdateTimetracking(string issuekey, int orginialEstimateMinutes, int remainingEstimateMinutes)
        {
            var request = new RestRequest()
            {
                Resource = $"{Urls.IssueByKey(issuekey)}",
                Method = Method.PUT,
                RequestFormat = DataFormat.Json,
            };

            // Alternative for "simple" fields
            //request.AddBody(
            //    new { fields = new { summary = issue.fields.summary } }
            //);

            request.AddJsonBody(
                new
                {
                    update = new
                    {
                        timetracking = new object[] {new
                        {
                            edit = new
                            {
                                originalEstimate = $"{orginialEstimateMinutes}m",
                                remainingEstimate= $"{remainingEstimateMinutes}m"
                            }
                        }}
                    }
                }
                );

            var response = _restClient.Execute(request);

            ValidateResponse(response);

            return response.StatusCode == HttpStatusCode.NoContent;
        }
        #region
        /// <summary>
	    /// Throws exception with details if request was not unsucessful
	    /// </summary>
	    /// <param name="response"></param>
	    private static void ValidateResponse(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed || response.ErrorException != null)
                throw new JiraClientException(
                    $"Response status: {response.ResponseStatus}\nHTTP response: {response.StatusCode}\nDescription: {response.StatusDescription}\nInner Content: {response.Content}");
        }

        /// <summary>
        /// Returns a comma separated string from the strings in the provided
        /// IEnumerable of strings. Returns an empty string if null is provided.
        /// </summary>
        /// <param name="strings">Comma-separated fields</param>
        /// <returns></returns>
        private static string GetFieldsToString(IEnumerable<string> fields) => fields != null ? string.Join(",", fields.ToArray()) : string.Empty;

        #endregion
    }
}
