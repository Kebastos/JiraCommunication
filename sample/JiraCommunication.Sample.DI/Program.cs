using JiraCommunication.Extensions;
using JiraCommunication.Sample.DI.HostedService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JiraCommunication.Sample.DI
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddJiraClient(new JiraCommunication.Commons.Options.JiraOptions()
                    {
                        ServerUrl = "https://jira.com",
                        UserName = "User.Name",
                        Password = "User-Password"
                    });

                    services.AddHostedService<AppHostedService>();
                });
    }
}
