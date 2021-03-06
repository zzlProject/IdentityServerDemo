﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api1
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
            //services.AddAuthorization();
            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(option =>
                    {
#if !DEBUG
                        option.Authority = "http://172.20.10.9:5000";
#endif
                        option.Authority = "http://localhost:5000";
                        option.RequireHttpsMetadata = false;
                        option.ApiName = "ApiRes1";
                        
                        option.SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Jwt;
                        
                    });
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
           
            app.UseMvc();
        }
    }
}
