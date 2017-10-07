using System;

namespace Timer.Abstractions
{
    public interface ICommand
    {
        DateTime TimeStamp { get; set; }
    }
}
