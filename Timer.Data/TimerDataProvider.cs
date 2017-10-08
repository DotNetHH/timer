using System;
using System.Collections.Generic;
using MJNsoft.Base.Database.Abstractions;
using MJNsoft.Base.DependencyInjection.Abstractions;
using Newtonsoft.Json;
using Timer.Data.Abstractions;
using System.Linq;
using MJNsoft.Base.Model;

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

        public IEnumerable<TimerEvent> GetAll()
        {
            return _commandRepositoryWorker.GetAll().Select(e => JsonConvert.DeserializeObject<TimerEvent>(e.Command));
        }

        public IEnumerable<TimerEvent> GetAllForDate(DateTime date)
        {
            var queryRequest = new QueryRequest<CommandEntity>();
            queryRequest.Where(e => e.TimeStamp.Date == date.Date);
            return _commandRepositoryWorker.Query(queryRequest).Select(e => JsonConvert.DeserializeObject<TimerEvent>(e.Command));
        }
    }
}
