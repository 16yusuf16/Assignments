using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HepsiBuradaAssignment.Api;
using HepsiBuradaAssignment.Api.App.AutoFacModule;
using HepsiBuradaAssignment.Api.App.ExceptionMiddleware;
using HepsiBuradaAssignment.Application.Commands;
using HepsiBuradaAssignment.Application.Queries;
using HepsiBuradaAssignment.Domain.Interfaces;
using HepsiBuradaAssignment.Infrastructure.Data.Context;
using HepsiBuradaAssignment.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HepsiBuradaAssignment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddScoped<IProductQueries, ProductQueries>();
       
            services.AddDbContext<AssignmentContext>(options => options.UseNpgsql(Configuration["ConnectionString"]));
            services.AddSwaggerGen(options =>
{
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "An ASP.NET Core Web API for managing ToDo items",
                    TermsOfService = new Uri("http://swagger.io/terms/"),
                });
                options.OperationFilter<SwaggerFileOperationFilter>();
            });
            services.AddScoped<ICampaignQueries, CampaignQueries>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddMediatR(
                typeof(CreateProductCommand),
                typeof(CreateCampaignCommand),
                typeof(CreateOrderCommand)
                );

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new ApplicationModule());
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
