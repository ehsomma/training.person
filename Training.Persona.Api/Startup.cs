namespace Training.Persona.Api
{
    using System;
    using System.IO;

    using Apollo.NetCore.Core.Web.Api;

    using Autofac;
    using Autofac.Extensions.DependencyInjection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;

    using Swashbuckle.AspNetCore.Swagger;

    /// <summary>
    /// Provides the entry point for an application.
    /// </summary>
    public class Startup
    {
        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="Startup"/> class.</summary>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        #endregion

        #region Properties

        /// <summary>Represents a set of key/value application configuration properties.</summary>
        public IConfiguration Configuration { get; }

        /// <summary>Creates, wires dependencies and manages lifetime for a set of components.</summary>
        public Autofac.IContainer ApplicationContainer { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// ConfigureServices is where you register dependencies. This gets called by the runtime 
        /// before the Configure method, below.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns>Un IServiceProvider.</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add services to the collection.
            IMvcBuilder mvcBuilder = services.AddMvc();

            // Configura el manejador global de excepciones.
            services.AddSingleton<GlobalExceptionFilter>();

            // Setup global exception filters.
            mvcBuilder.AddMvcOptions(
                options =>
                {
                    FilterCollection filters = options.Filters;
                    ServiceFilterAttribute exceptionFilter =
                        new ServiceFilterAttribute(typeof(GlobalExceptionFilter));
                    filters.Add(exceptionFilter);

                    // ServiceFilterAttribute validationFilter = new ServiceFilterAttribute(typeof(ValidateModelFilter));
                    filters.Add(typeof(ValidateModelFilter));
                });

            // Doc: https://github.com/domaindrivendev/Swashbuckle.AspNetCore
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Training.Persona.Api", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Training.Persona.Api.xml"));
                c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Training.Persona.Entities.xml"));
                c.DescribeAllEnumsAsStrings();
                c.IgnoreObsoleteActions();
                c.OperationFilter<SwaggerGenResponseFilter>();
            });

            // Create the container builder.
            ContainerBuilder builder = new ContainerBuilder();

            // Register dependencies, populate the services from the collection, and build the container.
            builder.Populate(services);

            // Registra la configuración (appsettings.json).
            builder.RegisterInstance(this.Configuration).AsImplementedInterfaces().SingleInstance();

            builder.RegisterModule<Persona.Ioc.PersonaModule>();

            // Registra y mapea los Settings (ConnectionStrings).
            builder.Register(
                    c =>
                    {
                        // Resuelve el ConfigurationRoot.
                        IConfigurationRoot configurationRoot = c.Resolve<IConfigurationRoot>();

                        // Obtiene la sección del .json de settings.
                        IConfigurationSection section = configurationRoot.GetSection("ConnectionStrings");

                        // Mapea la sección del .json a la clase.
                        Apollo.NetCore.Core.Settings.ConnectionStrings connStr = new Apollo.NetCore.Core.Settings.ConnectionStrings();
                        ConfigurationBinder.Bind(section, connStr);

                        return connStr;
                    }).As<Apollo.NetCore.Core.Settings.ConnectionStrings>()

                .SingleInstance();

            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Defines a class that provides the mechanisms to configure an application's request pipeline.</param>
        /// <param name="env">Provides information about the web hosting environment an application is running in.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Training.Persona.Api");
            });
        }

        #endregion
    }
}
