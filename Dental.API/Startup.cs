using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dental.Data.Data;
using Dental.Data.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Dental.API
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
            //services.AddMvc(setupAction =>
            //    {
            //        setupAction.Filters.Add(
            //            new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
            //        setupAction.Filters.Add(
            //            new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
            //        setupAction.Filters.Add(
            //            new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
            //        setupAction.Filters.Add(
            //            new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            //        setupAction.Filters.Add(
            //            new ProducesDefaultResponseTypeAttribute());

            //        setupAction.ReturnHttpNotAcceptable = true;

            //        setupAction.OutputFormatters.Add(new XmlSerializerOutputFormatter());

            //        var jsonOutputFormatter = setupAction.OutputFormatters
            //            .OfType<SystemTextJsonOutputFormatter>().FirstOrDefault();

            //        if (jsonOutputFormatter != null)
            //        {
            //            // remove text/json as it isn't the approved media type
            //            // for working with JSON at API level
            //            if (jsonOutputFormatter.SupportedMediaTypes.Contains("text/json"))
            //            {
            //                jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");
            //            }
            //        }
            //    })
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "LibraryOpenAPISpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Dental API",
                        Version = "1",
                        Description = "Through this API you can access Dental objects.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "cbfloryiv@gmail.com",
                            Name = "Curtis Flory",
                            Url = new Uri("https://cfloryiv.com")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

               setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/LibraryOpenAPISpecification/swagger.json",
                    "Dental API");
                setupAction.RoutePrefix = "";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var cs=new ScheduleUtility(context);
            cs.InitializeSchedule();
        }
    }
}
