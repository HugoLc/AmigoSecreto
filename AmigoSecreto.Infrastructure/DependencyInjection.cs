using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Infrastructure.Persistense.InMemo;
using AmigoSecreto.Infrastructure.Persistense.SqLite;
using Microsoft.Extensions.DependencyInjection;

namespace AmigoSecreto.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // services.AddScoped<IUserRepository, InMemoUserRepository>();
        services.AddScoped<IUserRepository, SqLiteUserRepository>();
        services.AddScoped<IGroupRepository, InMemoGroupRepository>();
        // services.AddScoped<IGroupRepository, InMemoGroupRepository>();
        services.AddScoped<IGroupRepository, SqLiteGroupRepository>();
        return services;
    }
}
