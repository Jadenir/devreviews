using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevReviews.API.Persistence;
using DevReviews.API.Persistence.Repositories;
using DevReviews.API.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DevReviews.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Pega a connectionString configurada no user-secrets
            //Configuration.GetConnectionString("DevReviewsCn");
            var connectionString = Configuration.GetValue<string>("DevReviewsCn");
            //Inicializa dbContext
            services.AddDbContext<DevReviewsDbContext>(o => o.UseSqlServer(connectionString));
            //Implementa o repositorio, e injeta na classe que implementou a interface
            services.AddScoped<IProductRepository, ProductRepository>();
            //Define automapper
            services.AddAutoMapper(typeof(ProductProfile));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevReviews.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevReviews.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
