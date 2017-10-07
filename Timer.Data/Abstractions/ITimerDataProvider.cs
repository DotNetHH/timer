using MJNsoft.Base.Database.Abstractions;

namespace Timer.Data
{
    public interface ITimerDataProvider
    {
        IRepository<CommandEntity> CommandRepository { get; }
    }
}
