using System;
using JiraCommunication.Commons.Options;

namespace JiraCommunication.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = GetOptions(args);   
            var client = new JiraCommunication.Services.JiraClient.JiraClientService(options);

            var projects = client.GetProjects();
            foreach (var project in projects)
            {
                Console.WriteLine(project.Name);
            }

            var issue = client.GetIssue("E4P-58");
            Console.WriteLine(issue);

            var createIssue = client.CreateIssue(new Commons.Models.CreateIssue("E4P", "sada", "sada-1", "1", "1", null));
            Console.WriteLine(createIssue);
        }

        private static JiraOptions GetOptions(string[] args)
        {
            return new JiraOptions()
            {
                ServerUrl= args[0],
                UserName =args[1],
                Password=args[2]
            };
        }

    }
}
