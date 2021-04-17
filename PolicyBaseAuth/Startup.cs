using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PolicyBaseAuth.Auth;
using PolicyBaseAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyBaseAuth
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<BookStoreContext>(cofig =>
            {
                cofig.UseSqlServer(Configuration.GetConnectionString("default"));
            });
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>();
            services.AddAuthorization(configure =>
            {
                var permissionType = typeof(Permissions);
                foreach (var item in permissionType.GetFields())
                {
                    var methodName = char.ToUpper(item.Name[0]) + item.Name.Substring(1);
                    var method = permissionType.GetMethod(methodName);
                    if (method != null)
                        configure.AddPolicy(item.Name,
                            config =>
                            {

                                method.Invoke(null, new[] { config });
                            });
                }
            });
            services.AddSingleton<IAuthorizationHandler, AdminReadBookHandler>();
            services.AddSingleton<IAuthorizationHandler, EditorReadBookHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
