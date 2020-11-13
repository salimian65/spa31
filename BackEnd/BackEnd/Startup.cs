using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;
using IdentityServer4.AccessTokenValidation;
using BackEnd.Authorization;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            services.AddDbContext<BackEndContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BackEndContext")));

            services.AddCors(options =>
                options.AddPolicy("frontend", builder =>
                    builder.WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                )
            );

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "backend";
                });

            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:5000";
            //        options.RequireHttpsMetadata = false;
            //        options.ApiName = "backend";
            //    });
         //   services.AddSingleton<IAuthorizationHandler, MyHandler1>();
           

            services.AddAuthorization(options => {
                options.AddPolicy("ProductOwner", policy => policy.Requirements.Add(new ProductOwnerAuthorizationRequirement()));
                options.AddPolicy("Customer", policy => policy.RequireClaim(JwtClaimTypes.Role, "customer"));
            });

            services.AddSingleton<IAuthorizationHandler, ProductOwnerAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("frontend");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
