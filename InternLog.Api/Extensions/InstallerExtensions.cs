using InternLog.Api.Installers;

namespace InternLog.Api.Extensions
{
    public static class InstallerExtensions
    {
        public static void InstallServicesFromAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Program).Assembly.ExportedTypes
            .Where(type => !type.IsInterface && type.IsAssignableTo(typeof(IServiceInstaller)) && !type.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IServiceInstaller>().ToList();

            installers.ForEach(installer => installer.InstallAsync(services, configuration));
        }
    }
}
