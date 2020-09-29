using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dental.UI.Data;
using Dental.UI.Services;

namespace Dental.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var DentalURI = new Uri("http://localhost:54512/");


            void RegisterTypedClient<TClient, TImplementation>(Uri apiBaseUrl)
                where TClient : class where TImplementation : class, TClient
            {
                services.AddHttpClient<TClient, TImplementation>(client =>
                {
                    client.BaseAddress = apiBaseUrl;
                });
            }

            // HTTP services
            RegisterTypedClient<IClientDataService, ClientDataService>(DentalURI);
            RegisterTypedClient<ICredentialDataService, CredentialDataService>(DentalURI);
            RegisterTypedClient<IMessageDataService, MessageDataService>(DentalURI);
            RegisterTypedClient<IBillDataService, BillDataService>(DentalURI);
            RegisterTypedClient<IAppointmentDataService, AppointmentDataService>(DentalURI);
            RegisterTypedClient<IPaySetupDataService, PaySetupDataService>(DentalURI);
            RegisterTypedClient<IChargeDataService, ChargeDataService>(DentalURI);
            RegisterTypedClient<IScheduleDataService, ScheduleDataService>(DentalURI);
            RegisterTypedClient<IProcedureDataService, ProcedureDataService>(DentalURI);
            RegisterTypedClient<IDoctorDataService, DoctorDataService>(DentalURI);
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
