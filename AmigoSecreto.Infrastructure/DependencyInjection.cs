using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Infrastructure.Persistense;
using Microsoft.Extensions.DependencyInjection;

namespace AmigoSecreto.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, InMemoUserRepository>();
        services.AddScoped<IGroupRepository, InMemoGroupRepository>();
        return services;
    }
}
