//using MJNsoft.Base.Database.Abstractions;
//using MJNsoft.Base.DependencyInjection;
//using NUnit.Framework;
//using Timer.Data.Abstractions;
//using System.Linq;

//namespace Timer.Data.Tests
//{
//    [TestFixture]
//    public class TimerDataProviderTests
//    {
//        private ITimerDataProvider _timerDataProvider;
//        private IRepositoryWorker<CommandEntity, TimerDbContext> _timerRepositoryWorker;

//        public TimerDataProviderTests()
//        {
//            _timerDataProvider = IoC.Services.GetService<ITimerDataProvider>();
//        }

//        [Test]
//        public void CommandRepository_ReturnsRepository()
//        {
//            _timerDataProvider.AddEvent(new TimerEvent { EventType = TimerEventType.Start, TimeStamp = new System.DateTime(2017, 10, 8, 9, 0, 0) });
//            _timerDataProvider.AddEvent(new TimerEvent { EventType = TimerEventType.Stop, TimeStamp = new System.DateTime(2017, 10, 8, 11, 0, 0) });

//            Assert.That(_timerDataProvider.GetAllForDate(new System.DateTime(2017,10,8)).Count(), Is.EqualTo(2));

//            // Aufräumen
//            var inserted = _timerRepositoryWorker.GetAll();
//            foreach(var @event in inserted)
//                _timerRepositoryWorker.Delete(@event.Id);
//        }
//    }
//}
