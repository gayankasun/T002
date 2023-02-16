using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using T002.API.Extensions;
using T002.API.T002.API;
using T002.Core.Interfaces;
using T002.Infrastructure.Data;
using T002.Infrastructure.Repositories;

namespace WEBAPI
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
            //var connectionStringsOptions =Configuration.GetSection("ConnectionStrings").Get<ConnectionStringsOptions>();
            //var cosmosDbOptions = Configuration.GetSection("CosmosDb").Get<CosmosDbOptions>();
            //var (serviceEndpoint, authKey) = connectionStringsOptions.ActiveConnectionStringOptions;
            //var (databaseName, collectionData) = cosmosDbOptions;
            //var collectionNames = collectionData.Select(c => c.Name).ToList();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WEBAPI", Version = "v1" });
            });
            //services.AddCosmosDb(serviceEndpoint, authKey, databaseName, collectionNames);
            services.AddScoped<IInvoiceHeaderRepository, InvoiceHeaderRepository>();
            services.AddScoped<ICosmosDbClientFactory, CosmosDbClientFactory>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEBAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
