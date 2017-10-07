using LinqToDB;
using MJNsoft.Base.Database.LinqToDb;
using MJNsoft.Base.Database.LinqToDb.Abstractions;
using MJNsoft.Base.Database.Migration.Abstractions;
using System;

namespace Timer.Data
{
    internal class TimerDbContext : DbContext,IMigratable
    {
        public Guid Id { get; } = new Guid("76DFD27B-93E0-4B32-8C1C-D72757F2D599");
        public string Name { get; } = "Timer";
        public int Version { get; set; } = 1; // Bei Änderungen der Struktur hochzählen

        public TimerDbContext(string configurationName)
          : base(configurationName)
        {
        }

        public ITable<CommandEntity> Person { get { return GetTable<CommandEntity>(); } }

        protected override void OnModelCreating(IDbModelBuilder modelBuilder)
        {
            // Registrierung der Entität(en) im Context
            // Optional: Spalten haben in der Datenbank einen anderen Namen
            modelBuilder.Entity<CommandEntity>()
                .HasTableName("Command")
                .HasSchemaName("dba")
                .HasPrimaryKey(e => e.Id);
                //.Property(p => p.Id).HasColumnName("id")
                //.Property(p => p.Command).HasColumnName("command");

        }

        public void Update(IMigrationUpdates updates, int newVersion)
        {
            switch (newVersion)
            {
                case 1:
                    updates.Add(@"CREATE TABLE dba.""Command""(""Id"" UUID NOT NULL
                                                             , ""Command"" TEXT NOT NULL
                                                             , PRIMARY KEY(""Id""))");
                    break;
            }
        }

    }

}
