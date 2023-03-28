using GeekBurger.Products.ExtensionMethods;
using GeekBurger.Products.Repositories;
using GeekBurger.Products.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace GeekBurger.Products
{
    public class Startup
    {
        public static IConfiguration Configuration;
        public IHostingEnvironment HostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Products", Version = "v1" });
            });

            
            services.AddAutoMapper(typeof(ProductsDbContext));

            services.AddDbContext<ProductsDbContext>
                (o => o.UseInMemoryDatabase("geekburger-products"));


            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductChangedEventRepository, ProductChangedEventRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IProductChangedService, ProductChangedService>();

            services.AddScoped<ILogService, LogService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ProductsDbContext productsDbContext)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            using (var serviceScope = app
                .ApplicationServices
                .GetService<IServiceScopeFactory>()
                .CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ProductsDbContext>();
                context.Database.EnsureCreated();
            }

            productsDbContext.Seed();
        }
    }
}