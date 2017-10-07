using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MJNsoft.Base.Database.Postgres.Abstractions;
using MJNsoft.Base.DependencyInjection;
using MJNsoft.Base.Log.Abstractions;
using Moq;
using Newtonsoft.Json;
using Timer.Abstractions;

namespace Timer.NaturalLanguage.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureServices();
            var commandManager = IoC.Services.GetService<ICommandManager>();

            System.Console.WriteLine("Zeiterfassung gestartet!");
            System.Console.WriteLine("Was möchten Sie tun?");
            System.Console.WriteLine("");
            System.Console.WriteLine("Beispiel: Starte Zeiterfassung");

            var service = new TimerNaturalLanguageService();

            bool isRunning = true;
            while (isRunning)
            {
                var sentence = System.Console.ReadLine();

                if(string.IsNullOrWhiteSpace(sentence))
                {
                    //nothing
                }
                else if (sentence == "exit")
                {
                    isRunning = false;
                }
                else if (sentence == "list")
                {
                    foreach (var command in commandManager.GetAll())
                    {
                        System.Console.WriteLine(command.GetType().Name + " " + JsonConvert.SerializeObject(command));
                    }
                }
                else
                {
                    try
                    {
                        var command = service.Analyse(sentence);
                        commandManager.AddCommand(command);

                        System.Console.WriteLine(command.GetType().Name + " " + JsonConvert.SerializeObject(command));
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("Exception: "+e.Message);
                    }
                }
            }

            //= args.Aggregate("", (i, j) => i + " " + j);
            
            
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

            //IoC.ServiceCollection.Add(services);
            //IoC.ServiceCollection.AddMvc();


            // ********************
            // Setup CORS
            // ********************
            //var corsBuilder = new CorsPolicyBuilder();
            //corsBuilder.AllowAnyHeader();
            //corsBuilder.AllowAnyMethod();
            //corsBuilder.AllowAnyOrigin(); // For anyone access.
            ////corsBuilder.WithOrigins("http://localhost:56573"); // for a specific url. Don't add a forward slash on the end!
            //corsBuilder.AllowCredentials();

            //IoC.ServiceCollection.AddCors(options =>
            //{
            //    options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            //});

            //IoC.ServiceCollection.AddCors();

            return IoC.Services;
        }
    }
}
