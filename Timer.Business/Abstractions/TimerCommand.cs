using System;

namespace Timer.Business.Abstractions
{
    public abstract class TimerCommand
    {
        public DateTime TimeStamp { get; set; }
    }
}
