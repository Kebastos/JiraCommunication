using System;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using JiraCommunication.Services.JiraClient;

namespace JiraCommunication.Sample.DI.HostedService
{
    public class AppHostedService : IHostedService
    {
        private readonly IJiraClientService _jiraClient;

        public AppHostedService(IJiraClientService jiraClient)
        {
            _jiraClient = jiraClient;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var projects = _jiraClient.GetProjects();
            foreach (var project in projects)
            {
                Console.WriteLine(project.Name);
            }

            var issue = _jiraClient.GetIssue("E4P-58");
            Console.WriteLine(issue);

            var createIssue = _jiraClient.CreateIssue(new Commons.Models.CreateIssue("E4P", "sada", "sada-1", "1", "1", null));
            Console.WriteLine(createIssue);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
