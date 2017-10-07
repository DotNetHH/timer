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

        //public void Get()
        //{
        //    _timerDataProvider.CommandRepository.Insert()
        //}
    }
}
