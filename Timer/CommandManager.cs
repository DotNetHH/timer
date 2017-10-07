using MJNsoft.Base.DependencyInjection.Abstractions;
using MJNsoft.Base.Services.Abstractions;
using Newtonsoft.Json;
using System;
using Timer.Abstractions;
using System.Collections.Generic;
using System.Linq;
using Timer.Data.Abstractions;

namespace Timer
{
    [AutoRegister]
    internal class CommandManager : ICommandManager
    {
        private ITimerDataProvider _timerDataProvider;
        private IDateTimeProvider _dateTimeProvider; 

        public CommandManager(ITimerDataProvider timerDataProvider, IDateTimeProvider dateTimeProvider)
        {
            _timerDataProvider = timerDataProvider;
            _dateTimeProvider = dateTimeProvider;
        }

        public void AddCommand(ICommand command)
        {
            var entity = new CommandEntity
            {
                TimeStamp = _dateTimeProvider.Now,
                Type = command.GetType().FullName,
                Command = JsonConvert.SerializeObject(command)
            };
            _timerDataProvider.CommandRepository.Insert(entity);
        }

        public IEnumerable<ICommand> GetAll()
        {
            var result = new List<ICommand>();

            _timerDataProvider.CommandRepository.GetAll().ToList().ForEach(e=>result.Add((ICommand)JsonConvert.DeserializeObject(e.Command,GetType().Assembly.GetType(e.Type))));

            return result;
        }
    }
}
