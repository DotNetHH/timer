using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MJNsoft.Base.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using MJNsoft.Base.Log.Abstractions;
using Timer;
using Timer.WebApi;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private Mock<ILoggerProvider> _loggerProviderMock = new Mock<ILoggerProvider>();
        private Mock<ILogger> _loggerMock = new Mock<ILogger>();

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            _loggerProviderMock.Setup(m => m.GetLogger(It.IsAny<string>())).Returns(_loggerMock.Object);
            _loggerProviderMock.Setup(m => m.GetLogger(It.IsAny<Type>())).Returns(_loggerMock.Object);
            _loggerMock.Setup(m => m.LogDebug(It.IsAny<string>())).Callback<string>(message => System.Diagnostics.Debug.WriteLine(message));

            IoC.ServiceCollection.AddSingleton<ILoggerProvider>(_loggerProviderMock.Object);
            
            IoC.ServiceCollection.Add(services);
            IoC.ServiceCollection.AddMvc();
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            var mapper = config.CreateMapper();

            // ********************
            // Setup CORS
            // ********************
            var corsBuilder = new CorsPolicyBuilder();

            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.

            //corsBuilder.WithOrigins("http://localhost:56573"); // for a specific url. Don't add a forward slash on the end!

            //corsBuilder.AllowCredentials();

            IoC.ServiceCollection.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            services.AddSingleton(mapper);

            IoC.ServiceCollection.Add(services);
            IoC.ServiceCollection.AddMvc();


            IoC.ServiceCollection.Add(services);
            IoC.ServiceCollection.AddMvc();


            return IoC.Services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("SiteCorsPolicy");
            app.UseMvc();

        }
    }
}
