using Microsoft.EntityFrameworkCore;
using Restaurantv2API.Entities;

namespace Restaurantv2API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();
            builder.Services.AddControllers();
            builder.Services.AddDbContext<RestaurantDbContext>();
            builder.Services.AddScoped<RestaurantSeeder>();
            builder.Services.AddDbContext<RestaurantDbContext>
                (options => options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDbConnection")));


            var app = builder.Build();

            //configure
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();

            seeder.Seed();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}