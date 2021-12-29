namespace InternLog.Api.Installers
{
    public interface IServiceInstaller
    {
        Task InstallAsync(IServiceCollection services, IConfiguration configuration);
    }
}
