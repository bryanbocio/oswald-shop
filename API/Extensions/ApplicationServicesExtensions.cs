using API.Errors;
using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuracion)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var options = ConfigurationOptions.Parse(configuracion.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(error => error.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
