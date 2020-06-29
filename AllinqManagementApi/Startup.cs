using AllinqManagementApi.Adapters;
using AllinqManagementApi.Interfaces;
using AllinqManagementApi.Services;
using DataModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AllinqManagementApi
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
            services.AddControllers();

            services.AddLogging(LoggerBuilder);

            services.AddSingleton<IHostedService, BroadCastServiceWorker>();

            services.AddSingleton<IFileService<Haspel>, CsvFileService>();
            services.AddSingleton<IFileService<string>, TeamService>();

            services.AddSingleton<IFileAdapter<Haspel>, CsvFileAdapter>();
            services.AddSingleton<IFileAdapter<string>, TeamsFileAdapter>();
        }

        private void LoggerBuilder(ILoggingBuilder builder)
        {
            builder.AddLog4Net();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
