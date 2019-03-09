

using AutoMapper;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

using Authenticatzo.Api.Extensions;

using Authenticatzo.Setup;
using Authenticatzo.Data.Database;
using Microsoft.EntityFrameworkCore;
using Authenticatzo.Api.ModelValidators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Authenticatzo.Api.Helpers;
using Authenticatzo.Api.ApiServices;

namespace Authenticatzo.Api
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                  .AddJsonOptions(o =>
                  {
                      if (o.SerializerSettings.ContractResolver != null)
                      {
                          var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                          castedResolver.NamingStrategy = null;
                      }
                  }).AddMvcOptions(o =>
                  {
                      o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                  });

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDbContext<Authenticatzo_DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Authenticatzo_DBContext")));
            services.AddTransient<VideoModelValidator, VideoModelValidator>();
            services.AddTransient<UserService, UserService>();
            services.AddCors(o => o.AddPolicy("CORSPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddAutoMapper();

            services.AddSwagger();
            services.AddDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseHsts();
            //}

            //app.UseMySwaggerConfig();

            //app.UseHttpsRedirection();
            //app.UseMvc();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("CORSPolicy");
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseMvc();
          
            app.UseHttpsRedirection();
            app.UseMySwaggerConfig();
            app.UseStaticFiles();
        }
    }
}
