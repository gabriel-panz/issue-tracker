namespace IssuesApi.Utils;

public static class ProgramConfigurationUtil
{
    public static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        var allInterfaces = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => t.IsInterface && t.FullName is not null &&
                (t.FullName.StartsWith("IssuesApi.Services.Interfaces")
                    || t.FullName.StartsWith("IssuesApi.Repositories.Interfaces")));

        var allClasses = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => t.IsClass && t.FullName is not null &&
            (t.FullName.StartsWith("IssuesApi.Services")
                || t.FullName.StartsWith("IssuesApi.Repositories")));

        foreach (var @interface in allInterfaces)
        {
            var classImplementation = allClasses
                .FirstOrDefault(c => c
                    .GetInterfaces()
                    .Any(x => x.Name == @interface.Name));

            if (classImplementation != null)
                services.AddScoped(@interface, classImplementation);
        }
    }
}
