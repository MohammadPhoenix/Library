using Library.Persistance.EF;
using Library.Persistance.EF.BookCategories;
using Library.Persistance.EF.Books;
using Library.Persistance.EF.LendingManagments;
using Library.Persistance.EF.MemberShips;
using Library.Services;
using Library.Services.BookCategories;
using Library.Services.BookCategories.Contracts;
using Library.Services.Books;
using Library.Services.Books.Contracts;
using Library.Services.LendingManagments;
using Library.Services.LendingManagments.Contracts;
using Library.Services.MemberShips;
using Library.Services.MemberShips.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.RestApi
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
            services.AddMvc();
            services.AddControllers();
            services.AddDbContext<EFDataContext>();
            services.AddScoped<BookCategoryRepository, EFBookCategoryRepository>();
            services.AddScoped<BookRepository, EFBookRepository>();
            services.AddScoped<MemberShipRepository, EFMemberShipRepository>();
            services.AddScoped<LendingManagmentRepository, EFLendingManagmentRepository>();
            services.AddScoped<BookService, BookAppService>();
            services.AddScoped<BookCategoryService, BookCategoryAppService>();
            services.AddScoped<MemberShipService, MemberShipAppService>();
            services.AddScoped<LendingManagmentService, LendingManagmentAppService>();
            services.AddScoped<UnitOfWork, EFUnitOfWorkRepository>();
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HttpReplApi v1");
            });
        }
    }
}
