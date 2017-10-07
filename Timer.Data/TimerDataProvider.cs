using MJNsoft.Base.Database.Abstractions;
using MJNsoft.Base.DependencyInjection.Abstractions;
using Timer.Data.Abstractions;

namespace Timer.Data
{
    [AutoRegister]
    internal class TimerDataProvider : ITimerDataProvider
    {
        public IRepository<CommandEntity> CommandRepository => _timerDbContext;
        private IRepositoryWorker<CommandEntity, TimerDbContext> _timerDbContext;
        public TimerDataProvider(IRepositoryWorker<CommandEntity, TimerDbContext> timerDbContext)
        {
            _timerDbContext = timerDbContext;
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
