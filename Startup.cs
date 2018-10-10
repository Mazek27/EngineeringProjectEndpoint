using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engineering_Project.Controllers;
using Engineering_Project.DataAccess;
using Engineering_Project.Extensions;
using Engineering_Project.Models.Domian;
using Engineering_Project.Service.Context;
using Engineering_Project.Service.Impement;
using Engineering_Project.Service.Interfaces;
using Engineering_Project.Service.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Swashbuckle.AspNetCore.Swagger;

namespace Engineering_Project
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
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));
            
            services.AddMvc();

#if DEBUG           
            services
                .AddDbContext<ApplicationContext>(options =>
                    options.UseNpgsql(Configuration.GetValue<string>("ConnectionStrings:postgresql_debug")));
#else
            services
                .AddDbContext<Context>(options =>
                    options.UseNpgsql(Configuration.GetValue<string>("ConnectionStrings:postgresql_release")));
#endif

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
            
            services.AddAuthentication(options => {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o => {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters{
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,

                        ValidIssuer = Configuration.GetValue<string>("TokenAuthentication:Issuer"),
                        ValidAudience = Configuration.GetValue<string>("TokenAuthentication:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("TokenAuthentication:SecretKey")))
                    };
                });
            
            
            services.AddTransient<IAccountDataAccess, AccountDataAccess>();
            
            //DataAccess
            services.AddTransient<ITrainingDataAccess, TrainingDataAccess>();
            
            //Services
            services.AddTransient<ITrainingService, TrainingService>();
            services.AddTransient<IInternationalizationService, InternationalizationService>();


            services.AddSwaggerDocumentation();
//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v2", new Info {Title = "eConService Api", Version = "v2"});
//                c.CustomSchemaIds(x => x.FullName);
//                 
////                string basePath = PlatformServices.Default.Application.ApplicationBasePath;
////                string xmlPath = Path.Combine(basePath, "eConServiceApi.xml"); 
////                c.IncludeXmlComments(xmlPath);
//                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
//            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationContext dbContext)
        {
            
            app.UseCors("AllowAll");

            app.UseAuthentication();
            
            app.UseStaticFiles();
            
            app.UseSwaggerDocumentation();
//            app.UseSwagger();
//            app.UseSwaggerUI(c =>
//            {
//                c.SwaggerEndpoint("/swagger/v2/swagger.json", "eConService Api V2");
//                c.InjectStylesheet(AppDomain.CurrentDomain.BaseDirectory, "outline");
//            });

            app.UseMvc();
        }
       
    }
}