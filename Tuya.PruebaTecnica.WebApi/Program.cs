using Microsoft.EntityFrameworkCore;
using Tuya.PruebaTecnica.Application.Features;
using Tuya.PruebaTecnica.Application.Interfaces;
using Tuya.PruebaTecnica.Application.Services;
using Tuya.PruebaTecnica.Infraestructure.Persistence.Context;
using Tuya.PruebaTecnica.Infraestructure.Persistence.Repositories;
using Tuya.PruebaTecnica.WebApi.Midlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<ICustomerServices, CustomerServices>();
builder.Services.AddTransient<IProductServices, ProductServices>();
builder.Services.AddTransient<IOrderService, OrderService>();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySQL(
        builder.Configuration.GetConnectionString("conexionDB"),
        builder => builder.MigrationsAssembly("Tuya.PruebaTecnica.Infraestructure.Persistence")
    );
});

//cors
var misreglasCors = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misreglasCors, builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors(misreglasCors);
app.UseAuthorization();
app.MapControllers();

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}