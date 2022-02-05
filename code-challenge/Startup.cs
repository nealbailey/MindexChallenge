using challenge.Data;
using challenge.Repositories;
using challenge.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace code_challenge
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
            services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseInMemoryDatabase("EmployeeDB");
            });

            // For Swagger
            services.AddSwaggerGen();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "ChallengeApi", Version = "v1" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddScoped<IEmployeeRepository,EmployeeRespository>();
            services.AddScoped<IReportingStructureRepository, ReportingStructureRepository>();
            services.AddTransient<EmployeeDataSeeder>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IReportingStructureService, ReportingStructureService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, EmployeeDataSeeder seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seeder.Seed().Wait();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Challenge API V1");
                });
            }
            app.UseMvc();
        }
    }
}
