using MJNsoft.Base.Services.Abstractions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Timer.Business;
using Timer.Business.Abstractions;
using Timer.Data.Abstractions;

namespace Timer.Tests
{

    [TestFixture]
    class ReaderManagerTests
    {

        private Mock<ITimerDataProvider> _timerDataProviderMock = new Mock<ITimerDataProvider>();
        private Mock<IDateTimeProvider> _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        private DateTime _today = new DateTime(2099, 4, 28);
        public ReaderManagerTests()
        {
            //_timerDataProviderMock.Setup(m => m.CommandRepository).Returns(_commandRepositoryMock.Object);
            _dateTimeProviderMock.Setup(m => m.Today).Returns(_today);

        }


        [Test]
        public void TestStartStopHours()
        {
            _timerDataProviderMock.Setup(m => m.GetAllForDate(It.IsAny<DateTime>())).Returns(new List<TimerEvent> {
    new TimerEvent {
        EventType = TimerEventType.Start,
        TimeStamp = _today.AddHours(9).AddMinutes(10)
    },
    new TimerEvent {
        EventType = TimerEventType.Stop,
        TimeStamp = _today.AddHours(10).AddMinutes(40)
    }
            });

            var readerManager = CreateReaderManager();
            Assert.That(readerManager.GetAllHoursForToday(), Is.EqualTo(1.5));
        }

        private IReaderManager CreateReaderManager()
        {
            return new ReaderManager(_timerDataProviderMock.Object, _dateTimeProviderMock.Object);
        }
    }
}
