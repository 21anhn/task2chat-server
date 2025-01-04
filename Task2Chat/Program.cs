
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task2Chat.Data;

namespace Task2Chat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var appDb = builder.Configuration["ConnectionStrings:ApplicationDb"];
            var authDb = builder.Configuration["ConnectionStrings:AuthDb"];

            builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(appDb));
            builder.Services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(authDb));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                            .AddEntityFrameworkStores<AuthDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Seed data
            await SeedDatabaseAsync(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static async Task SeedDatabaseAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                await SeedData.InitializeAsync(services);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }


}
