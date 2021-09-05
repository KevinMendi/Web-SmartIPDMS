using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueCliMiddleware;
using Newtonsoft.Json.Serialization;


using Microsoft.EntityFrameworkCore;
using ipdms.Models.AppDbContext;

namespace ipdms
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
            ////Enable CORS
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //});

            ////JSON Serializer
            //services.AddControllers().AddJsonOptions(options => {
            //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //    options.JsonSerializerOptions.DictionaryKeyPolicy = null;

            //});
           

            services.AddControllers();
            var a = Configuration.GetConnectionString("MoviesDbConnectionString");
            //services.AddDbContext<MoviesDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MoviesDbConnectionString")));
            //Console.WriteLine(a);
            //var connection = @"Server=DESKTOP-UJOQJ6I;Database=MoviesDb;Trusted_Connection=True;ConnectRetryCount=0";
            var options = new DbContextOptionsBuilder<IpdmsDbContext>();
            options.UseSqlServer(a);

            ////services.AddDbContext<MoviesDbContext>(options => options.UseSqlServer(connection));
            //services.AddDbContext<MoviesDbContext>(options => options.UseSqlServer(connection));

            using (var context = new IpdmsDbContext(options.Options))
            {
                // do stuff
                Console.WriteLine("Suuloooooooooodddd");
                Console.WriteLine("Suuloooooooooodddd");
                services.AddDbContext<IpdmsDbContext>(
                    options => options.UseSqlServer(a)

                    );

                
            }

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ////Enable CORS
            //app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSpaStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                    spa.Options.SourcePath = "ClientApp/";
                else
                    spa.Options.SourcePath = "dist";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve");
                }

            });
        }
    }
}
