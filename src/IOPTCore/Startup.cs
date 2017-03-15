using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IOPTCore.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace IOPTCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = Configuration.GetConnectionString("DataAccessPostgreSqlProvider");

            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(
                    sqlConnectionString,
                    b => b.MigrationsAssembly("IOPTCore")
                )
            );
            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "Logging/logger.txt"));
            //var logger = loggerFactory.CreateLogger("FileLogger");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "session",
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute("Model", "snapshot/{*route}", new { controller = "RESTful", action = "Model" });
                routes.MapRoute("Main", "Main", new { controller = "Home", action = "Index" });
                routes.MapRoute("Login", "login", new { controller = "Home", action = "Login" });
                routes.MapRoute("Logout", "Logout", new { controller = "Home", action = "Logout" });
                routes.MapRoute("default", "", new { controller = "Home", action = "Login" });
                //routes.MapRoute("ModelEdit", "View/{*slugs}", new { controller = "ModelEdit", action = "GetView" });
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}");

            });

            //TestData.GenerateTestData(app.ApplicationServices);


            var context = app.ApplicationServices.GetService<DataContext>();

            foreach (var m in context.Models)
            {
                Snapshot.current.models.Add(m);
            }
            foreach (var m in Snapshot.current.models)
                foreach (var o in context.Objects)
                    if (m.id == o.modelId) m.objects.Add(o);

            foreach (var m in Snapshot.current.models)
                foreach (var o in m.objects)
                    foreach (var p in context.Properties)
                        if (o.id == p.objectId) o.properties.Add(p);

            foreach (var m in Snapshot.current.models)
                foreach (var o in m.objects)
                    foreach (var p in o.properties)
                        foreach (var s in context.Scripts)
                            if (p.id == s.propertyId) p.scripts.Add(s);


            //foreach (var o in context.Objects) o.Properties.AddRange(from p in context.Properties where p.Object == o select p);
            // foreach (var p in context.Properties) p.Scripts.AddRange(from s in context.Scripts where s.Property == p select s);
            //foreach (var o in context.Objects)
            //{
            //    if ((from t in o.Model.Objects where t.Id == o.Id select t).Count() == 0)
            //        o.Model.Objects.Add(o);
            //}
            //foreach (var p in context.Properties)
            //{
            //    if ((from t in p.Object.Properties where t.Id == p.Id select t).Count() == 0)
            //        p.Object.Properties.Add(p);
            //}
            //foreach (var s in context.Scripts)
            //{
            //    if ((from t in s.Property.Scripts where t.Id == s.Id select t).Count() == 0)
            //        s.Property.Scripts.Add(s);
            //}

        }
    }
}
