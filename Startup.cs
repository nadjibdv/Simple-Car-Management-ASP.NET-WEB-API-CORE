using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebCarApi1.Models;
using WebCarApi1.Models.Repository;

namespace WebCarApi1
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




            services.AddCors(c => {

                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            });


            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CarWebApi", Version = "v1" });
            
            });
            services.AddMvc();

           services.AddDirectoryBrowser(); // >>>=

            services.AddScoped<ICarPersonRepository<Person>, PersonDbRepository>();
            services.AddScoped<ICarPersonRepository<Car>, CarDbRepositor>();

            services.AddDbContext<MyDbContext>(options => {
                options.UseMySQL(Configuration.GetConnectionString("MyConnection"));
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            // app.UseStaticFiles();
            app.UseDefaultFiles();

           app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"Photos/car")),
                    RequestPath = new PathString("/images")
            });

            //app.UseFileServer();

          /*  app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                      Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
                RequestPath = "",
                EnableDefaultFiles = true
            });*/


            app.UseHttpsRedirection();


           

            app.UseRouting();

            app.UseAuthorization();


            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarWebApi"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
