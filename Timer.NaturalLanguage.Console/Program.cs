using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MJNsoft.Base.Database.Postgres.Abstractions;
using MJNsoft.Base.DependencyInjection;
using MJNsoft.Base.Log.Abstractions;
using Moq;

namespace Timer.NaturalLanguage.Console
{
    partial class Program
    {
        static void Main(string[] args)
        {
            ConfigureServices();

            try
            {
                IoC.Services.GetService<ConsoleInputHandler>().Start();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }
        }



        private static Mock<ILoggerProvider> _loggerProviderMock = new Mock<ILoggerProvider>();
        private static Mock<ILogger> _loggerMock = new Mock<ILogger>();

        // This method gets called by the runtime. Use this method to add services to the container.
        public static IServiceProvider ConfigureServices()
        {
            // use type to load assembly for IoC
            var type = typeof(IPostgresAdministrator);
            var assembly = type.Assembly;

            _loggerProviderMock.Setup(m => m.GetLogger(It.IsAny<string>())).Returns(_loggerMock.Object);
            _loggerProviderMock.Setup(m => m.GetLogger(It.IsAny<Type>())).Returns(_loggerMock.Object);
            _loggerMock.Setup(m => m.LogDebug(It.IsAny<string>())).Callback<string>(message => System.Diagnostics.Debug.WriteLine(message));

            IoC.ServiceCollection.AddSingleton<ILoggerProvider>(_loggerProviderMock.Object);

            IoC.ServiceCollection.AddSingleton<TimerNaturalLanguageService>();
            IoC.ServiceCollection.AddSingleton<ISentenceHandler, SentenceHandler>();
            IoC.ServiceCollection.AddSingleton<ConsoleInputHandler>();

            return IoC.Services;
        }
    }
}
