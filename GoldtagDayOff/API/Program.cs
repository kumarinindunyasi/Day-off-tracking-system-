using Microsoft.EntityFrameworkCore;
using API.Service.IzinServices;
using API.Service.PersonsServices;
using API.Context;
using API.Utilities;
using API.Service.EmailSenderServices;
using Microsoft.AspNetCore.ResponseCompression;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            Logger.Log(message: $"Host başlatılıyor.", LogFileType.Application, LogLevelType.Info);
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PERSON")));
            builder.Services.AddScoped<DbContext, AppDbContext>();

            builder.Services.AddScoped<IPersonsService, PersonsService>();
            builder.Services.AddScoped<IOffService, OffService>();


            builder.Services.AddControllers().AddJsonOptions(opt => { opt.JsonSerializerOptions.PropertyNamingPolicy = null; });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddResponseCompression(opt =>
            {
                opt.EnableForHttps = true;
                opt.Providers.Add<GzipCompressionProvider>();
            });
            builder.Services.Configure<GzipCompressionProviderOptions>(opt =>
            {
                opt.Level = System.IO.Compression.CompressionLevel.Optimal;
            });



            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();

            builder.Services.AddTransient<IEmailSenderService, EmailSenderService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseResponseCompression();
            app.UseRouting();
            app.UseHttpsRedirection();

            app.Run();
        }
        catch (Exception ex)
        {
            Logger.Log(message: $"Host başlatılamadı. Ex:{ex}", LogFileType.Application, LogLevelType.Error);
        }
    }
}