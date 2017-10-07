
using System.Collections.Generic;

namespace Timer.Abstractions
{
    public interface ICommandManager
    {
        void AddCommand(ICommand command);

        IEnumerable<ICommand> GetAll();
    }
}
