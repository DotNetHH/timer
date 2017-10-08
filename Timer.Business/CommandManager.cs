using MJNsoft.Base.DependencyInjection.Abstractions;
using MJNsoft.Base.Services.Abstractions;
using System;
using Timer.Business.Abstractions;
using System.Collections.Generic;
using Timer.Data.Abstractions;

namespace Timer.Business
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

        public void AddWriterCommand(WriterCommand command)
        {
            TimerEventType eventType = TimerEventType.Start;
            var commandType = command.GetType();

            switch( command)
            {
                case StartTaskCommand startCommand:
                    eventType = TimerEventType.Start;
                    break;
                case StopTaskCommand stopCommand:
                    eventType = TimerEventType.Stop;
                    break;
                case InterruptTaskCommand interruptCommand:
                    eventType = TimerEventType.Interrupt;
                    break;
                default:
                    throw new ArgumentException(nameof(command));

            }

            _timerDataProvider.AddEvent(new TimerEvent
            {
                EventType = eventType,
                TimeStamp = command.TimeStamp,
                Description = command.Description,
                TicketId = command.TicketId
            });
        }

        public IEnumerable<TimerCommand> GetAll()
        {
            var result = new List<TimerCommand>();

            //_timerDataProvider.CommandRepository.GetAll().ToList().ForEach(e=>result.Add((TimerCommand)JsonConvert.DeserializeObject(e.Command,GetType().Assembly.GetType(e.Type))));

            return result;
        }
    }
}
