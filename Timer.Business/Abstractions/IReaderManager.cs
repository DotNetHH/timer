using System;
using System.Collections.Generic;
using System.Text;
using Timer.Data.Abstractions;

namespace Timer.Business.Abstractions
{
    public interface IReaderManager
    {
        double GetAllHoursForToday();

        IEnumerable<TimerEvent> GetAll();
    }
}
