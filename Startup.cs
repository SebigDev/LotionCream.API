using LotionCream.API.Data;
using LotionCream.API.Services.Categories;
using LotionCream.API.Services.Comments;
using LotionCream.API.Services.Posts;
using LotionCream.API.Services.ProductLists;
using LotionCream.API.Services.Products;
using LotionCream.API.Services.Replies;
using LotionCream.API.Services.UserManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace LotionCream.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
              services.AddDbContext<LotionCreamDBContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("LotionCreamDbContextConn")));

            //Injection of services
            services.AddScoped<IUserServices, UserServices>(); //Scoped for Authentication Issues
            services.AddTransient<ICategoryServices,CategoryServices>();
            services.AddTransient<IPostServices, PostServices>();
            services.AddTransient<ICommentServices, CommentServices>();
            services.AddTransient<IReplyServices,ReplyServices>(); 
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<IProductListService, ProductListService>();

            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Skin Hub API", Version = "v1" });
            });
            
            

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

             app.UseCors(builder => builder.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin()
                                    .AllowCredentials());
            app.UseHttpsRedirection();
            app.UseSpaStaticFiles();
            app.UseStaticFiles();
            app.UseSwagger();
            
            
              app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Skin Hub API v1");
                c.RoutePrefix = string.Empty;
            });
           app.UseMvc();
            // app.UseMvc(routes =>
            //             {
            //                 routes.MapRoute(
            //                     name: "default",
            //                     template: "{controller}/{action=Index}/{id?}");
            //             });
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}