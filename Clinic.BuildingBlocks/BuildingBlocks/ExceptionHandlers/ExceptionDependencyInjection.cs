using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.ExceptionHandlers;

public static class ExceptionDependencyInjection
{
    public static IServiceCollection AddCustomExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<DomainExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        
        services.AddProblemDetails();

        return services;
    }
}