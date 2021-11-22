using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pfm.Services;
using pfm.Database.Contracts;
using pfm.Database;

namespace pfm
{
    public class Startup
    {
        
        private IServiceCollection Services { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get;}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "pfm", Version = "v1" });
            });

            services.AddDbContext<pfm_databaseContext>(x =>
            {
                x.UseNpgsql("Host=localhost;Database=pfm_database;User Id=postgres;Password=Stanislava99.");
            });
            
            services.AddControllers();
            services.AddHttpClient();
            services.AddCors();
            // services.AddAutoMapper(typeof(Startup).Assembly);

            // Dependency injection resolvers for services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICategoriesService, CategoriesService>();

            services.AddAutoMapper(typeof(Startup));

            Services = services;
          }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "pfm v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
