using MJNsoft.Base.Database.Postgres.Abstractions;
using MJNsoft.Base.DependencyInjection;
using System;

namespace CreateDatabase
{
    class Program
    {
        private static IPostgresAdministrator _dbAdmistrator;

        static void Main(string[] args)
        {
            Console.WriteLine("Creating Database...");

            _dbAdmistrator = IoC.Services.GetService<IPostgresAdministrator>();

            _dbAdmistrator.CreateDatabase("127.0.0.1", 6432, "Timer", "postgres", "", "dba", "sql");
        }

    }
}
