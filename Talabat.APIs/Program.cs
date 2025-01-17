using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Helpers;
using Talabat.Core.Repositories;
using Talabat.Repsitory;
using Talabat.Repsitory.Data;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region ServicesConfiguration Add services to the container

            builder.Services.AddControllers(); //Web APIs
            //Swagger/OpenAPI 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add services to the container.
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                //set connection string in appsettings.json
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles())); //old solution
            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            #endregion

            var app = builder.Build();

            #region Update-Database
            // //invalid => create a new instance of StoreContext

            //StoreContext dbContext = new StoreContext(); 
            //await dbContext.Database.MigrateAsync();

            // // CLR object => StoreContext [explcitly]

            var Scope = app.Services.CreateScope(); // group of services Life Time [scoped services]
            var Services = Scope.ServiceProvider; // get the services
            var LoggerFactory = Services.GetService<ILoggerFactory>();

            try
            {
                // get the StoreContex service
                var dbContext = Services.GetRequiredService<StoreContext>(); 
                await dbContext.Database.MigrateAsync();

                #region Seed-Database

                await StoreContextSeed.SeedAsync(dbContext);

                #endregion
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory?.CreateLogger<Program>();
                Logger?.LogError(ex, "An error occurred during migration");
                //Console.WriteLine(ex.Message);
            }

            #endregion

            

            #region Configure- Configure the HTTP request pipeline
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();

        }
    }
}
