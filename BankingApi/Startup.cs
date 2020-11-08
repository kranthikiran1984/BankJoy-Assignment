using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BankingApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace BankingApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }

        private IServiceProvider RegisterServices(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            //populate Autofac container builder with registered services
            containerBuilder.Populate(services);

            var result = new List<Type>();
            var types = Assembly.GetExecutingAssembly().GetTypes();

            if (types != null)
            {
                foreach (var type in types)
                {
                    if (type == typeof(IDependencyRegistrar)
                            && typeof(IDependencyRegistrar).IsAssignableFrom(type)
                            && !type.IsInterface
                            && !type.IsAbstract
                            && type.IsClass)
                        continue;

                    result.Add(type);
                }

                foreach (var resultType in result)
                {
                    ((IDependencyRegistrar)resultType).Register(containerBuilder);
                }
            }

            var _serviceProvider = new AutofacServiceProvider(containerBuilder.Build());

            return _serviceProvider;
        }
    }
}
