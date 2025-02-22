using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using ITLab29.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ITLab29.Data.Repositories;
using ITLab29.Models.Domain;
using ITLab29.Filters;

namespace ITLab29 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(@"Server=.;Database=ITLab;Integrated Security=True;"));
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<DataInitializer>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMediaRepository, MediaRepository>();
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<LoggedOnUserFilter>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<DataInitializer>();
            services.AddRouting(options => options.LowercaseUrls = true);
            //services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataInitializer initializer) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseSession();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Session}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            initializer.InitializeData().Wait();
        }
    }
}
