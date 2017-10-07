using MJNsoft.Base.DependencyInjection;
using NUnit.Framework;
using Timer.Data.Abstractions;

namespace Timer.Data.Tests
{
    [TestFixture]
    public class TimerDataProviderTests
    {
        [Test]
        public void CommandRepository_ReturnsRepository()
        {
            Assert.That(IoC.Services.GetService<ITimerDataProvider>().CommandRepository, Is.Not.Null);
        }
    }
}
