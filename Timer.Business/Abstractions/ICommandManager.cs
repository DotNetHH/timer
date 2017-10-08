
using System.Collections.Generic;

namespace Timer.Business.Abstractions
{
    public interface ICommandManager
    {
        void AddWriterCommand(WriterCommand command);

        IEnumerable<TimerCommand> GetAll();
    }
}
