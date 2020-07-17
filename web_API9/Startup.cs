using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using web_API9.Infrastructure;
using Microsoft.OpenApi.Models;
using web_API9.Models.Application.User;
using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.MongoDB;

namespace web_API9
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
            services.AddControllersWithViews();

            services.AddIdentity<UserWithIdentity, MongoIdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Home/NoSecret";
            });

            services
                .Configure<MongoDBOption>(Configuration.GetSection("MongoDBOption"))
                .AddMongoDatabase()
                .AddMongoDbContext<UserWithIdentity, MongoIdentityRole>()
                .AddMongoStore<UserWithIdentity, MongoIdentityRole>();

            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    //config.Cookie.Name = "Grandmas.Cookie";
                    //config.LoginPath = "/Home/Authenticate";

                    config.Cookie.Name = "Identity.Cookie";
                    config.LoginPath = "/Home/NoSecret";
                });

            //services.AddAuthorization();

            services.Configure<MongoBDOSettings>(
                Configuration.GetSection(nameof(MongoBDOSettings)));
            services.AddSingleton<IMongoBDO>(sp =>
                sp.GetRequiredService<IOptions<MongoBDOSettings>>().Value);

            services.AddSingleton<DeploymentService>();
            services.AddSingleton<Userservice>();
            services.AddSingleton<ProjectService>();
            services.AddSingleton<DatabaseService>();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "web_API9", Version = "v1" });
            });

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "web_API9");
            });
        }
    }
}
