using MJNsoft.Base.DependencyInjection.Abstractions;
using System;
using Timer.Abstractions;
using Timer.Data;

namespace Timer
{
    [AutoRegister]
    internal class CommandManager:ICommandManager
    {
        private ITimerDataProvider _timerDataProvider;

        public CommandManager(ITimerDataProvider timerDataProvider)
        {
            _timerDataProvider = timerDataProvider;
        }

        public void AddCommand(ICommand command)
        {
            //var entity = new CommandEntity
            //{

            //    Command = Newtonsoft.Json.SerializeObject(command);
            //}

            //throw new NotImplementedException();
        }

        //public void Get()
        //{
        //    _timerDataProvider.CommandRepository.Insert()
        //}
    }
}
