using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuestionsAndAnswers.Extensions;
using QuestionsAndAnswers.Hubs;
using QuestionsAndAnswers.Models;
using QuestionsAndAnswers.Config;

namespace QuestionsAndAnswers
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public static Settings Config { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Settings>(Configuration);
            Config = Configuration.Get<Settings>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:58837/")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
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

            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<QuestionsAndAnswersHub>(Config.HubName);
            });

            // Force database seeding to execute
            context.Database.EnsureCreated();
        }
    }
}
