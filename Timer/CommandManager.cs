using MJNsoft.Base.DependencyInjection.Abstractions;
using MJNsoft.Base.Services.Abstractions;
using Newtonsoft.Json;
using System;
using Timer.Abstractions;
using Timer.Data;

namespace Timer
{
    [AutoRegister]
    internal class CommandManager : ICommandManager
    {
        private ITimerDataProvider _timerDataProvider;
        private IDateTimeProvider _dateTimeProvider; 

        public CommandManager(ITimerDataProvider timerDataProvider)
        {
            _timerDataProvider = timerDataProvider;
        }

        public void AddCommand(ICommand command)
        {
            var entity = new CommandEntity
            {
                TimeStamp = _dateTimeProvider.Now,
                Command = JsonConvert.SerializeObject(command)
            };
            _timerDataProvider.CommandRepository.Insert(entity);
        }
    }
}
