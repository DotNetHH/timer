
namespace Timer.Data.Abstractions
{
    // Hexagonale Architektur: Interfaces für die Datenzugriffsschicht im Business-Layer
    public interface ITimerDataProvider
    {
        void AddEvent(TimerEvent timerEvent);
    }
}
