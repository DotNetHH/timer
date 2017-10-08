using MJNsoft.Base.Database.Abstractions;
using MJNsoft.Base.DependencyInjection.Abstractions;
using Newtonsoft.Json;
using Timer.Data.Abstractions;

namespace Timer.Data
{
    [AutoRegister]
    internal class TimerDataProvider : ITimerDataProvider
    {
        public IRepository<CommandEntity> CommandRepository => _commandRepositoryWorker;
        private IRepositoryWorker<CommandEntity, TimerDbContext> _commandRepositoryWorker;
        public TimerDataProvider(IRepositoryWorker<CommandEntity, TimerDbContext> commandRepositoryWorker)
        {
            _commandRepositoryWorker = commandRepositoryWorker;
        }

        public void AddEvent(TimerEvent timerEvent)
        {
            _commandRepositoryWorker.Insert(new CommandEntity
            {
                TimeStamp = timerEvent.TimeStamp,
                Command = JsonConvert.SerializeObject(timerEvent)
            });
        }

        //private void Test()
        //{
        //    using (var ctx = _timerDbContext.CreateContext())
        //    {
        //        ctx.BeginTransaction();
        //        _timerDbContext.Insert()

        //            ctx.CommitTransaction();
        //    }
        //}


    }
}
