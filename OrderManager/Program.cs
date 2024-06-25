using Database;
using Microsoft.EntityFrameworkCore;
using OrderManager.Services;

var builder = WebApplication.CreateBuilder(args);


IConfiguration Configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, true)
    .AddJsonFile($"appsettings.Development.json", optional: true, true)
    .Build();

builder.Services.AddDbContext<OrdersDbContext>(options =>
{
    options.UseNpgsql(Configuration.GetConnectionString("PostgresConnection"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseRouting();

app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller=Orders}/{action}/{id?}");

app.UseAuthorization();

app.UseCors("AllowLocalhost3000");

app.Run();