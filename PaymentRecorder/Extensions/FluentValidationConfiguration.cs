using Common.Dto;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace PaymentRecorder.Extensions;

public static class FluentValidationConfiguration
{
    public static IServiceCollection ConfigureFluentValidation(this IServiceCollection services)
    {
        
        services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = true;
        });

        services.AddValidatorsFromAssemblyContaining<DtoAssemblyMarkerForDependencyInjection>(ServiceLifetime.Transient);


        return services;
    }
}