using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjektAPI.Models;
using System.Collections.Generic;

namespace ProjektAPI
{
    public class Startup
    {
        private const string APIKEYNAME = "ApiKey";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KinoAPI", Version = "2.0" });

                c.AddSecurityDefinition(APIKEYNAME, new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Name = APIKEYNAME,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Prosze podać klucz:",
                });

                var key = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = APIKEYNAME
                    },
                    In = ParameterLocation.Header
                };

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement()
                    {
                        {
                            key, new List<string>()
                        }
                    });
            });
            services.AddDbContext<APIDatabaseContext>(config => config.UseSqlServer(Configuration.GetConnectionString("APIDatabaseContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KinoAPI 2.0"));
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