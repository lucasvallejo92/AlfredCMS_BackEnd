using AlfredCMS.Core.Models;
using AlfredCMS.Core.Repositories;
using AlfredCMS.Core.Repositories.Interfaces;
using AlfredCMS.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AlfredCMS
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<DataContext>(options => options.UseMySQL("server=localhost;port=32774;user=root;password=123456;database=alfred_cms"));

            // Injecting Automapper and defining their options
            services.AddAutoMapper(options =>
            {
                options.CreateMap<CategoryDTO, Categories>();
                options.CreateMap<PostDTO, Posts>();
                options.CreateMap<UserDTO, Users>();
            }, typeof(Startup).Assembly);

            // Setting up the auth config
            string securityKey = "abcdefgabcdefgabcdefg";
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //what to validate
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        //setup validate data
                        ValidIssuer = "AlfredCMS",
                        IssuerSigningKey = symmetricSecurityKey
                    };
                });

            services.AddMvc().AddJsonOptions(ConfigureJson);

            // Relationships
            services.AddTransient<IRepository<CategoryDTO>, CategoryRepository>();
            services.AddTransient<IRepository<PostDTO>, PostRepository>();
            services.AddTransient<IUserRepository<UserDTO>, UserRepository>();
        }

        private void ConfigureJson(MvcJsonOptions obj)
        {
            obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
