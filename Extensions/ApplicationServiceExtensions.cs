using URLShortenerService.Models.Repos;
using URLShortenerService.Interfaces;
using URLShortenerService.Models.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace URLShortenerService.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
         IConfiguration config)
        {

            //CORS Fix
            services.AddCors(options =>
            {
                options.AddPolicy("AllowClientApp",
                    builder => builder.WithOrigins("http://localhost:5184")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            //Database setup
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            
            //Application Service Registration
            services.AddTransient<IMetroURL, EFMetroURL>();

            return services;
        }
    }
}