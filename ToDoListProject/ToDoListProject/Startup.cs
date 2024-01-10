using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ToDoListProject.Data;
using ToDoListProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ToDoListProject.Services;

namespace ToDoListProject
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // For SQLite Database
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // For In-Memory Database
            //services.AddDbContext<AppDbContext>(options =>
            //    options.UseInMemoryDatabase("ToDoList"));

            // Set up dependency injection for the service layer.
            services.AddScoped<IToDoItemService, ToDoItemService>();

            // Register the Swagger generator, defining one or more Swagger documents.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoList API", Version = "v1" });
            });

            // Add CORS services and define the policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("https://nsfk7n-4200.csb.app",
                                            "https://frontenddomain2.com",
                                            "https://frontenddomain3.com") // Replace with your frontend domains
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Enable CORS with the specified policy
            app.UseCors("AllowSpecificOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve Swagger-UI specified JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList API V1");
                c.RoutePrefix = string.Empty; // To serve the Swagger UI at the app's root (http://localhost:<port>/)
            });
        }
    }
}
