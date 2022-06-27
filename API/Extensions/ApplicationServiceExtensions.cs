using AspNetCoreRateLimit;
using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection service) =>
            service.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void AddApplicationService(this IServiceCollection service)
        {
            //service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //service.AddScoped<IProductoRepository, ProductRepository>();
            //service.AddScoped<IMarcaRepository, MarcaRepository>();
            //service.AddScoped<ICategoriaRepository, CategoriaRepository>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureRateLimitiong(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();
            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.EnableEndpointRateLimiting = true;
                opt.StackBlockedRequests = false;
                opt.HttpStatusCode = 429;
                opt.RealIpHeader = "X-Real-IP";
                opt.ClientIdHeader = "X-ClientId";
                opt.GeneralRules = new List<RateLimitRule> { 
                    new RateLimitRule
                    {
                        Endpoint="*",
                        Period="10s",
                        Limit=10
                    }
                };
            });
        }
        
        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion=new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                //opt.ApiVersionReader = new QueryStringApiVersionReader("ver");
                opt.ApiVersionReader = new HeaderApiVersionReader("X-Version");
                opt.ReportApiVersions = true;

            });
        }
    }
}
