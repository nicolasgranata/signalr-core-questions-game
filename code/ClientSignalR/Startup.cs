using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TypeScriptClient.Models;
using Microsoft.Extensions.Configuration;

namespace TypeScriptClient
{
    public class Startup
    {        
        public IConfiguration Configuration { get; }
        private static SettingServerModel Settings { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SettingServerModel>(Configuration.GetSection("SettingServer"));
            Settings = Configuration.GetSection("SettingServer").Get<SettingServerModel>();
            string uriSignalR = Settings.Uri;

            services.AddCors(options => 
            {
                options.AddPolicy(Settings.CorsPolicity,
                builder =>
                {
                    builder.WithOrigins(Settings.Uri)
                                       .AllowAnyHeader()
                                       .WithMethods("GET", "POST");
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(Settings.CorsPolicity);

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
