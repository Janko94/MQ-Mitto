using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Interface.Service;
using Model;
using Service.HandleService;
using Service.IoC;
using Swashbuckle.AspNetCore.Swagger;
using Interface.Repository;
using Repository;
using Interface.Business;
using Business;

namespace CAP
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
            services.AddDbContext<MittoContext>(o => o.UseSqlServer(Configuration["ConnectionString"]));
            services.AddCap(x =>
            {
                x.UseKafka(Configuration["MQ:Server"]);
                x.DefaultGroupName = Configuration["MQ:DefaultGroup"];
                //x.DefaultGroup = Configuration["MQ:DefaultGroup"];
                x.UseEntityFramework<MittoContext>();
            });
            services.AddSwaggerGen();

            IoCContainer.Initialize(Configuration);
            RegisterBusiness(services);
            RegisterRepository(services);
        }

        private void RegisterRepository(IServiceCollection services)
        {
            services.AddScoped<ICountryCodeRepository, CountryCodeRepository>();
            services.AddScoped<ISMSRepository, SMSRepository>();
        }

        private void RegisterBusiness(IServiceCollection services)
        {
            services.AddScoped<ICountryCodeBusiness, CountryCodeBusiness>();
            services.AddScoped<ISMSBusiness, SMSBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(/*c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            }*/);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
