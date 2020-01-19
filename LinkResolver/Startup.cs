using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkResolver.Models.Data;
using LinkResolver.Models.Data.Repositories;
using LinkResolver.Models.Dto.Requests;
using LinkResolver.Models.Gateways;
using LinkResolver.Models.Gateways.Interfaces;
using LinkResolver.Models.UseCases.Interfaces;
using LinkResolver.Models.UseCases.Link;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LinkResolver
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //DB
            string constr = Configuration.GetConnectionString("Default");
            services.AddDbContext<LinkDBContext>(option => option.UseSqlServer(constr));

            //DI
            services.AddTransient<ILinkManager, LinkManager>();
            services.AddTransient<ILinkEFManager, LinkEFManager>();
            services.AddTransient<ICodeGenerator, SimpleCodeGenerator>();
            services.AddTransient<ICommandHandler<LinkSaveCommand, string>, LinkSaveHandler>();
            services.AddTransient<ICommandHandler<LinkResolveCommand, string>, LinkResolveHandler>();

            //Config
            var linkMgrOptions = Configuration.GetSection("LinkManagerOptions").Get<LinkManagerOptions>();
            services.AddSingleton<LinkManagerOptions>(linkMgrOptions);

            //Swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });

            //Cors
            string allowedOriginHost = Configuration.GetValue<string>("AllowedHosts");
            services.AddCors(config =>
               config.AddPolicy("DefaultPolicy",
               policy =>
               {
                   policy.WithOrigins(allowedOriginHost).AllowAnyMethod().AllowAnyHeader();
               })
            );

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseCors("DefaultPolicy");
            app.UseMvc();
        }
    }
}
