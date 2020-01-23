using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.WebApi.Catalog.Middleware;
using Service.WebApi.Catalog.Services;
using Service.WebApi.Catalog.ViewModels;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

// © fatahyuda@gmail.com - free to use

namespace Service.WebApi.Catalog
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
            services.AddScoped<ITokenManager, TokenManager>();

            services.AddSingleton<IAuthorizationHandler, DepartmentHandler>();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "Catalog Service", Version = "v1", Description = "Web Api Catalog" });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });

            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<CredentialAttr>(appSettingSection);

            var appSettings = appSettingSection.Get<CredentialAttr>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var issuer = appSettings.Issuer;

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
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = false
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthConst.Department_Finance, policy =>
                    policy.Requirements.Add(new DepartmentRequirement(AuthConst.Department_Finance)));
                options.AddPolicy(AuthConst.Department_IT, policy =>
                    policy.Requirements.Add(new DepartmentRequirement(AuthConst.Department_IT)));
                options.AddPolicy(AuthConst.Department_Sales, policy =>
                    policy.Requirements.Add(new DepartmentRequirement(AuthConst.Department_Sales)));
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
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog Service v1");
                c.DocumentTitle = "Title Documentation";
                c.DocExpansion(DocExpansion.None);
            });
            app.UseMvc();
        }
    }
}
