using System.Text.Json.Serialization;
using app.Data;
using app.Data.Repositories;
using app.Middleware;
using app.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddXmlSerializerFormatters()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                }); ;

            services.AddSingleton<DbContextFactory>();
            services.AddDbContext<DataContext>(options =>
                        options.UseMySql(
                            Configuration.GetConnectionString("default"),
                            ServerVersion.AutoDetect(Configuration.GetConnectionString("default"))));

            
            services.AddSingleton(sp =>
            {
                var factory = sp.GetRequiredService<DbContextFactory>();
                var cache = new UsersCache(factory);
                return cache;
            });

            services.AddScoped<UserRepository>();
            services.AddScoped<UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
