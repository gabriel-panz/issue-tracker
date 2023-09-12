using IssuesApi.Classes.Base;
using IssuesApi.Classes.Base.Interfaces;

namespace IssuesApi.Utils;

public static class ProgramConfigurationUtil
{
    public static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        var allInterfaces = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => t.IsInterface &&
                (t.FullName.StartsWith("IssuesApi.Services.Interfaces")
                    || t.FullName.StartsWith("IssuesApi.Repositories.Interfaces"))
                && !t.FullName.Contains("ServiceBase")
                && !t.FullName.Contains("RepositoryBase"));

        var allClass = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => t.IsClass
            && (t.FullName.StartsWith("IssuesApi.Services")
                || t.FullName.StartsWith("IssuesApi.Repositories"))
            && !t.FullName.Contains("ServiceBase")
            && !t.FullName.Contains("RepositoryBase"));

        foreach (var intfc in allInterfaces)
        {
            var impl = allClass
                .FirstOrDefault(c => c
                    .GetInterfaces()
                    .Any(x => x.Name == intfc.Name));

            if (impl != null)
                services.AddScoped(intfc, impl);
        }
    }
}
