using GitIssueManager.Core.Strategies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace GitIssueManager.Core.Configuration
{
    public static class ServiceConfigurator
    {
        public static void AddServiceStrategies(this IServiceCollection services, IConfiguration config)
        {
            var svcOpts = new ServiceConfigurationOptions();
            config.GetSection("IssueServices").Bind(svcOpts);

            if (svcOpts.GitHub?.Enabled == true)
            {
                services.AddHttpClient<IServiceStrategy, GitHubStrategy>(c =>
                {
                    c.BaseAddress = new Uri(svcOpts.GitHub.Url);
                    c.DefaultRequestHeaders.UserAgent.ParseAdd(svcOpts.GitHub.UserAgent);
                    c.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("token", svcOpts.GitHub.Token);
                });

                services.AddSingleton<IServiceStrategy, GitHubStrategy>();
            }

            if (svcOpts.GitLab?.Enabled == true)
            {
                services.AddHttpClient<IServiceStrategy, GitLabStrategy>(c =>
                {
                    c.BaseAddress = new Uri(svcOpts.GitLab.Url);
                    c.DefaultRequestHeaders.UserAgent.ParseAdd(svcOpts.GitLab.UserAgent);
                    c.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", svcOpts.GitLab.Token);
                });
                services.AddSingleton<IServiceStrategy, GitLabStrategy>();
            }
        }
    }
}
