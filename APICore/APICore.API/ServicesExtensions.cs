using APICore.API.Utils.JsonLocalization;
using APICore.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace APICore.API
{
    public static class ServicesExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config)
        {

            /*services.AddDbContextPool<CoreDbContext>(
                dbContextOptions => dbContextOptions
                    .UseSqlServer(
                        // Replace with your connection string.
                        config.GetConnectionString("ApiConnection"))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );*/


            services.AddDbContextPool<CoreDbContext>(
                dbContextOptions => dbContextOptions
                    .UseInMemoryDatabase(databaseName: "Tasks")
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // define swagger docs and other options
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Core",
                    Version = "v1",
                    Description = "API Core"
                });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter JWT Bearer authorization token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lowercase!!!
                    BearerFormat = "Bearer {token}",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        // defines scope - without a protocol use an empty array for global scope
                        { securityScheme, Array.Empty<string>() }
                    }
                );
                var basePath = AppContext.BaseDirectory;
                var fileName = Path.Combine(basePath, "APICore.API.xml");
                var fileName2 = Path.Combine(basePath, "APICore.Common.xml");
                options.IncludeXmlComments(fileName);
                options.IncludeXmlComments(fileName2);
            });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
           {
               options.AddDefaultPolicy(builder =>
                   builder.SetIsOriginAllowed(_ => true)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials());
           });
        }

        public static void ConfigureI18N(this IServiceCollection services)
        {
            #region Localization
            services.AddLocalization(o =>
            {
                // We will put our translations in a folder called Resources
                o.ResourcesPath = "i18n";
            });
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix,
                opts => { opts.ResourcesPath = "i18n"; })
            .AddDataAnnotationsLocalization(options =>
            {
            });
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            #endregion Localization
        }

        public static void ConfigureHsts(this IServiceCollection services)
        {
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });
        }
    }
}