
using System;
using System.Collections.Generic;

namespace Timer.Data.Abstractions
{
    // Hexagonale Architektur: Interfaces für die Datenzugriffsschicht im Business-Layer
    public interface ITimerDataProvider
    {
        void AddEvent(TimerEvent timerEvent);

        IEnumerable<TimerEvent> GetAll();

        IEnumerable<TimerEvent> GetAllForDate(DateTime date);
    }
}
