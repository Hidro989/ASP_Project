using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using ThiTracNghiem.Data;
using ThiTracNghiem.Services;

namespace ThiTracNghiem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<TracNghiemContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));
            builder.Services.AddScoped<IMonThiRepository, MonThiRepository>();
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //.AddJsonOptions(x =>
            //    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddCors(option =>
            //{
            //    option.AddPolicy(name: "MyAllowSpecificOrigins", policy =>
            //    {
            //        policy.WithOrigins("http://127.0.0.1:5500/ThiTracNghiem")
            //        .AllowAnyHeader()
            //        .AllowAnyMethod(); ;
            //    });
            //});
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            //app.UseCors("MyAllowSpecificOrigins");
            app.UseAuthorization();
            

            app.MapControllers();
            
            app.Run();
        }
    }
}