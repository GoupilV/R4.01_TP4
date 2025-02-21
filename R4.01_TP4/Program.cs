
using Microsoft.EntityFrameworkCore;
using R4._01_TP4.Models.DataManager;
using R4._01_TP4.Models.EntityFramework;
using R4._01_TP4.Models.Repository;

namespace R4._01_TP4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<FilmsRatingDBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("FilmRatingsDBContext")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Config repository dependancy injection 
            builder.Services.AddScoped<IDataRepository<Utilisateur>, UtilisateurManager>();

            var app = builder.Build();

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
    }
}
