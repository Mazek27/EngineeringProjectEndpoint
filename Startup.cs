﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engineering_Project.Controllers;
using Engineering_Project.DataAccess;
using Engineering_Project.Service.Context;
using Engineering_Project.Service.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            services
                .AddDbContext<Context>(options =>
                    options.UseNpgsql(SqlBuild().ToString()));
            
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<Context>();
            
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
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new Info {Title = "eConService Api", Version = "v2"});
                c.CustomSchemaIds(x => x.FullName);
                 
//                string basePath = PlatformServices.Default.Application.ApplicationBasePath;
//                string xmlPath = Path.Combine(basePath, "eConServiceApi.xml"); 
//                c.IncludeXmlComments(xmlPath);
//                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            app.UseCors("AllowAll");

            app.UseAuthentication();
            
            app.UseStaticFiles();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "eConService Api V2");
                c.InjectStylesheet(AppDomain.CurrentDomain.BaseDirectory, "outline");
            });

            app.UseMvc();
        }

        private NpgsqlConnectionStringBuilder SqlBuild()
        {
            NpgsqlConnectionStringBuilder sqlBuilder = new NpgsqlConnectionStringBuilder();
            sqlBuilder.Username = "odqeztrsgnunyo";
            sqlBuilder.Password = "a0da1a9c71bf56b78fae0936b858e3d3e367a157c8486f6a5fc77e1f9f32d596";
            sqlBuilder.Host = "ec2-54-217-243-160.eu-west-1.compute.amazonaws.com";
            sqlBuilder.Port = Int32.Parse("5432");
            sqlBuilder.Database = "d57jgurlic9ipb";
            sqlBuilder.Pooling = true;
            sqlBuilder.UseSslStream = true;     
            sqlBuilder.SslMode = Npgsql.SslMode.Require;
            sqlBuilder.TrustServerCertificate = true;

            return sqlBuilder;
        }
    }
}