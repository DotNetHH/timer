//using MJNsoft.Base.Database.Abstractions;
//using MJNsoft.Base.Database.Postgres.Abstractions;
//using MJNsoft.Base.DependencyInjection;
//using MJNsoft.Base.Log.Abstractions;
//using Microsoft.Extensions.DependencyInjection;
//using Moq;
//using NUnit.Framework;
//using System;

//namespace Timer.Data.Tests
//{
//    [SetUpFixture]
//    public class TestSetup
//    {
//        private const string AdminUser = "postgres";
//        private const string AdminPassword = "";

//        private IPostgresAdministrator _dbAdministrator;
//        private ConnectionInfo _connectionInfo;

//        private Mock<ILoggerProvider> _loggerProviderMock = new Mock<ILoggerProvider>();
//        private Mock<ILogger> _loggerMock = new Mock<ILogger>();
//        public TestSetup()
//        {
//            _loggerProviderMock.Setup(m => m.GetLogger(It.IsAny<string>())).Returns(_loggerMock.Object);
//            _loggerProviderMock.Setup(m => m.GetLogger(It.IsAny<Type>())).Returns(_loggerMock.Object);
//            _loggerMock.Setup(m => m.LogDebug(It.IsAny<string>())).Callback<string>(message => System.Diagnostics.Debug.WriteLine(message));

//            IoC.ServiceCollection.AddSingleton<ILoggerProvider>(_loggerProviderMock.Object);


//            _dbAdministrator = IoC.Services.GetService<IPostgresAdministrator>();

//            var dbConfiguration = IoC.Services.GetService<IDatabaseConfiguration>();

//            _connectionInfo = _dbAdministrator.AnalyseConnectionString(dbConfiguration.ConnectionString);
//        }

//        [OneTimeSetUp]
//        public void Initialize()
//        {
//            Cleanup();

//            _dbAdministrator.CreateDatabase(_connectionInfo.Server, _connectionInfo.Port, _connectionInfo.Database, AdminUser, AdminPassword, _connectionInfo.User, _connectionInfo.Password);
//        }

//        [OneTimeTearDown]
//        public void Cleanup()
//        {
//            _dbAdministrator.DeleteDatabase(_connectionInfo.Server, _connectionInfo.Port, _connectionInfo.Database, AdminUser, AdminPassword);
//        }

//    }
//}
