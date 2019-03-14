using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpeedWar.Data;
using SpeedWar.Hubs;
using SpeedWar.Models.Interfaces;
using SpeedWar.Models.Services;

namespace SpeedWar
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            services.AddSignalR();
            services.AddDbContext<CardDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDeckCardManager, DeckCardMgmtSvc>();
            services.AddScoped<IUserManager, UserMgmtSvc>();

        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseMvc();
            app.UseCookiePolicy();
            app.UseSignalR(routes =>
            {
                routes.MapHub<PlayHub>("/PlayHub");
            });
        }
    }
}
