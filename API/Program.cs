using API.Extensions;
using AspNetCoreRateLimit;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.ConfigureRateLimitiong();
builder.Services.ConfigureApiVersioning();


// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.AddApplicationService();

//Acepta solo formatos JSON y XML
builder.Services.AddControllers(opt =>
{
    opt.RespectBrowserAcceptHeader = true;
    opt.ReturnHttpNotAcceptable = true;
}).AddXmlSerializerFormatters();


builder.Services.AddDbContext<TiendaContext>(
        options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseIpRateLimiting();
app.UseApiVersioning();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope=app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggetFactory=services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<TiendaContext>();
        await context.Database.MigrateAsync();
        await TiendaContextSeed.SeedAsync(context, loggetFactory);
        
    }
    catch(Exception ex)
    {
        var logger = loggetFactory.CreateLogger<Program>();
        logger.LogError(ex, "Error durante la migracion");
    }
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
