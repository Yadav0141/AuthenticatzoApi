﻿

using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.DependencyInjection;

namespace Authenticatzo.Api.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection _iServiceCollection)
        {
            _iServiceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1.0.0", new Info
                {
                    Version = "V1.0.0",
                    Title = "Authenticatzo",
                    Description = "Categorization API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Abhishek Kumar",
                       
                        Url = "https://www.authenticatzo.azurewebsites.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return _iServiceCollection;
        }

        public static IApplicationBuilder UseMySwaggerConfig(this IApplicationBuilder _iApplicationBuilder)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            _iApplicationBuilder.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            _iApplicationBuilder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1.0.0/swagger.json", ".NET Core API V1.0.0");
                c.RoutePrefix = string.Empty;
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DefaultModelsExpandDepth(-1);
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);
                c.EnableDeepLinking();
                c.EnableFilter();
                c.ShowExtensions();
                c.EnableValidator();
            });

            return _iApplicationBuilder;
        }
    }
}
