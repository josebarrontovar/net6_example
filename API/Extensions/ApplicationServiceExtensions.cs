using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;

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
    }
}
