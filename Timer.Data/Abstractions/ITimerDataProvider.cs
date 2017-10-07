using MJNsoft.Base.Database.Abstractions;

namespace Timer.Data.Abstractions
{
    public interface ITimerDataProvider
    {
        IRepository<CommandEntity> CommandRepository { get; }
    }
}
