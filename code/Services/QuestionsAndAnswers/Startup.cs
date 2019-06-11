using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuestionsAndAnswers.Extensions;
using QuestionsAndAnswers.Hubs;
using QuestionsAndAnswers.Models;

namespace QuestionsAndAnswers
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public static SettingClientModel Settings { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SettingClientModel>(Configuration.GetSection("SettingClient"));
            Settings = Configuration.GetSection("SettingClient").Get<SettingClientModel>();
            services.AddCors(options =>
            {
                options.AddPolicy(Settings.CorsPolicity,
                builder =>
                {
                    builder.WithOrigins(Settings.Uri)
                                        .AllowAnyHeader()
                                        .WithMethods("GET","POST");
                });
              
            services.AddDbContext<QnADbContext>(option =>
            {
                option.UseInMemoryDatabase("QnaSignalR");

            });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, QnADbContext context)
        {
            app.ConfigureExceptionHandler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins(Settings.Uri)
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST")
                    .AllowCredentials();
            });


            app.UseSignalR(routes =>
            {
                routes.MapHub<QuestionsAndAnswersHub>(Settings.HubName);
            });

            // Force database seeding to execute
            context.Database.EnsureCreated();
        }
    }
}
