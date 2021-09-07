using JiraCommunication.Commons.Models;
using RestSharp;
using System.Collections.Generic;
using Version = JiraCommunication.Commons.Models.Version;

namespace JiraCommunication.Services.JiraClient
{
    /// <summary>
    /// The interface implements the logic of working with Jira
    /// </summary>
    public interface IJiraClientService
    {
        /// <summary>
        /// Executes a request and returns the deserialized response. 
        /// If the response hasn't got the specified expected response code or if an exception was thrown during execution a JiraClientException will be  thrown.
        /// </summary>
        /// <typeparam name="T">Request return type</typeparam>
        /// <param name="request">Request to execute</param>
        /// <param name="expectedResponseCode">The expected HTTP response code</param>
        /// <returns>Deserialized response of request</returns>
        T Execute<T>(RestRequest request) where T : new();

        /// <summary>
        /// Returns the Issue with the specified key. If the fields parameters specified only the given field names will be loaded.  
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="issueKey">Issue key</param>
        /// <param name="fields">Fields to load</param>
        /// <returns>
        /// The issue with the specified key or null if no such issue was found.
        /// </returns>
        Issue GetIssue(string issueKey, IEnumerable<string> fields = null);

        /// <summary>
        /// Returns True if the change was successful, or False if for some reason it was not possible to update the fields.
        /// </summary>
        /// <param name="issuekey">Issue key</param>
        /// <param name="fields">Fields to load</param>
        /// <returns>True - if the update was successful. False - if there are problems for some reason.</returns>
        bool UpdateIssueFields(string issuekey, object fields);

        /// <summary>
		/// Searches for Issues using JQL. 
        /// Throws a JiraClientException if the request was unable to execute.
		/// </summary>
		/// <param name="jql">JQL</param>
		/// <param name="startAt">Start at...</param>
		/// <param name="maxResults">The maximum number of returned results</param>
		/// <param name="fields">Fields to search</param>
		/// <returns>The results of the search performed by the JQL query</returns>
		Issues GetIssuesByJql(string jql, int startAt, int maxResults, IEnumerable<string> fields = null);

        /// <summary>
        /// Returns the Issues for the specified project.  
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="projectKey">Project key</param>
        /// <param name="startAt">Start at...</param>
        /// <param name="maxResults">The maximum number of returned results</param>
        /// <param name="fields">Fields to search</param>
        /// <returns>All Issues of the specified project</returns>
        Issues GetIssuesByProject(string projectKey, int startAt, int maxResults, IEnumerable<string> fields = null);

        /// <summary>
        /// Returns all available projects the current user has permision to view. 
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <returns>Details of all projects visible to user</returns>
        List<Project> GetProjects();

        /// <summary>
        /// Returns a list of all possible priorities.  
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <returns>All possible priorities</returns>
        List<Priority> GetPriorities();

        /// <summary>
        /// Returns the meta data for creating issues. 
        /// This includes the available projects and issue types, but not fields (fields are supported in the Jira api but not by this wrapper currently).
        /// </summary>
        /// <param name="projectKey">Project key</param>
        /// <returns>The meta data for creating issues</returns>
        ProjectMeta GetProjectMeta(string projectKey);

        /// <summary>
        /// Returns a list of all possible issue statuses. 
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <returns>All possible issue statuses</returns>
        List<Status> GetStatuses();

        /// <summary>
        /// Creates and returns a new Version.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="newVersion">Parameters of the new Version</param>
        /// <returns>Created Version</returns>
        Version CreateVersion(NewVersion newVersion);

        /// <summary>
        /// Updates the specified version.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="version">Version parameters to update</param>
        /// <returns>Updated Version</returns>
        Version UpdateVersion(UpdateVersion version);

        /// <summary>
        /// Returns all versions of the specified project.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="projectKey">Project key</param>
        /// <returns>All versions of the specified project</returns>
        IEnumerable<Version> GetVersions(string projectKey);

        /// <summary>
        /// Creates a new issue.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="newIssue">Parameters of the new issue</param>
        /// <returns>The new issue</returns>
        BasicIssue CreateIssue(CreateIssue newIssue);

        /// <summary>
        /// Returns the application property with the specified key.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="propertyKey">The property key</param>
        /// <returns>The application property with the specified key</returns>
        ApplicationProperty GetApplicationProperty(string propertyKey);

        /// <summary>
        /// Returns the attachment with the specified id.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="attachmentId">Attachment id</param>
        /// <returns>The attachment with the specified id</returns>
        Attachment GetAttachment(string attachmentId);

        /// <summary>
        /// Deletes the specified attachment.
        /// Throws a JiraClientException if the request was unable to execute.
        /// </summary>
        /// <param name="attachmentId">Attachment id to delete</param>
        bool DeleteAttachment(string attachmentId);

        /// <summary>
        /// Update time tracking estimates
        /// </summary>
        /// <param name="issuekey">Issue key</param>
        /// <param name="orginialEstimateMinutes">Original estimate(minutes)</param>
        /// <param name="remainingEstimateMinutes">Remaining estimate(minutes)</param>
        /// <returns></returns>
        bool UpdateTimetracking(string issuekey, int orginialEstimateMinutes, int remainingEstimateMinutes);
    }
}
