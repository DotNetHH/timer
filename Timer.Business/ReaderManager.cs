using MJNsoft.Base.DependencyInjection.Abstractions;
using MJNsoft.Base.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timer.Business.Abstractions;
using Timer.Data.Abstractions;

namespace Timer.Business
{
    // auf Leseseite keine Commands mehr verwendet, da nicht "geloggt" werden muss, sondern alles direkt im Manager als Methode abgewickelt
    // AutoRegister für Auflösung der Abhängigkeiten via DependencyInjection im Konstruktor der abhängigen Klassen
    [AutoRegister]
    internal class ReaderManager : IReaderManager
    {
        private ITimerDataProvider _timerDataProvider;
        private IDateTimeProvider _dateTimeProvider;

        public ReaderManager(ITimerDataProvider timerDataProvider, IDateTimeProvider dateTimeProvider)
        {
            _timerDataProvider = timerDataProvider;
            _dateTimeProvider = dateTimeProvider;
        }

        public IEnumerable<TimerEvent> GetAll()
        {
            return _timerDataProvider.GetAll();
        }

        public double GetAllHoursForToday()
        {
            // nach Zeitstempel sortiert
            
            double result = 0;
            var listOfEvents = _timerDataProvider.GetAllForDate(_dateTimeProvider.Today).OrderBy(e => e.TimeStamp);
            DateTime? currentTimeStamp = null;

            foreach (var @event in listOfEvents)
            {
                if (!currentTimeStamp.HasValue && @event.EventType == TimerEventType.Start)
                {
                    currentTimeStamp = @event.TimeStamp;
                }
                else if (currentTimeStamp.HasValue && @event.EventType == TimerEventType.Stop)
                {
                    result += (@event.TimeStamp - currentTimeStamp.Value).TotalHours;
                    currentTimeStamp = null;
                }
                // Tageswechsel --> erstes Event am Tag ist ein Stop, daher Stunden Mitternacht bis Stopevent
                else if (!currentTimeStamp.HasValue && @event.EventType == TimerEventType.Stop)
                {
                    result += (currentTimeStamp.Value - currentTimeStamp.Value.Date).TotalHours;
                }

            }

            // Tageswechsel --> letztes Event am Tag ist ein Start, daher Stunden Startevent bis Mitternacht
            if (currentTimeStamp.HasValue)
            {
                result += (currentTimeStamp.Value.AddDays(1).Date - currentTimeStamp.Value).TotalHours;
            }

            return result;
        }
    }
}
