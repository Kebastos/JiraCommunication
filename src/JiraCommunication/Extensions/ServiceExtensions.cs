using JiraCommunication.Commons.Options;
using JiraCommunication.Services.JiraClient;
using Microsoft.Extensions.DependencyInjection;

namespace JiraCommunication.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add a service for working with Jira.
        /// </summary>
        /// <param name="jiraOptions">Jira connection options</param>
        /// <returns></returns>
        public static IServiceCollection AddJiraClient(this IServiceCollection services, JiraOptions jiraOptions)
        {
            services.AddOptions().Configure<JiraOptions>(x =>
            {
                x.ServerUrl = jiraOptions.ServerUrl;
                x.UserName = jiraOptions.UserName;
                x.Password = jiraOptions.Password;
            });
            services.AddSingleton<IJiraClientService, JiraClientService>();

            return services;
        }
    }
}
