using MJNsoft.Base.Database.Abstractions;
using MJNsoft.Base.Services.Abstractions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Timer.Abstractions;
using Timer.Data.Abstractions;

namespace Timer.Tests
{
    [TestFixture]
    public class CommandManagerTests
    {
        private Mock<ITimerDataProvider> _timerDataProviderMock = new Mock<ITimerDataProvider>();
        private Mock<IDateTimeProvider> _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        private Mock<IRepository<CommandEntity>> _commandRepositoryMock = new Mock<IRepository<CommandEntity>>();

        private DateTime now = new DateTime(2100, 12, 31);

        public CommandManagerTests()
        {
            _timerDataProviderMock.Setup(m => m.CommandRepository).Returns(_commandRepositoryMock.Object);
            _dateTimeProviderMock.Setup(m => m.Now).Returns(now);
        }

        [Test]
        public void AddCommand()
        {
            var commandManager = CreateCommandManager();

            CommandEntity insertedEntity = null;

            _commandRepositoryMock.Setup(m => m.Insert(It.IsAny<CommandEntity>())).Callback<CommandEntity>(entity => insertedEntity = entity);

            commandManager.AddCommand(new StartTaskCommand { TimeStamp = new DateTime(2020, 1, 1), Description="Test", TicketId="TicketId" });

            Assert.That(insertedEntity, Is.Not.Null);
            Assert.That(insertedEntity.TimeStamp, Is.EqualTo(now));
            Assert.That(typeof(ICommand).Assembly.GetType(insertedEntity.Type), Is.EqualTo(typeof(StartTaskCommand)));
            //            Assert.That(insertedEntity.Command, Is.EqualTo(new DateTime(2020, 1, 1)));
        }

        [Test]
        public void GetAll()
        {
            var commandManager = CreateCommandManager();

            _commandRepositoryMock.Setup(m => m.GetAll()).Returns(new List<CommandEntity> { new CommandEntity { Type = "Timer.Abstractions.StopTaskCommand", Command = "{}" } });

            var commands = commandManager.GetAll();

            Assert.That(commands.Single(), Is.TypeOf<StopTaskCommand>());
        }

        private ICommandManager CreateCommandManager()
        {
            return new CommandManager(_timerDataProviderMock.Object, _dateTimeProviderMock.Object);

        }
    }
}
