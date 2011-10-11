using FubuTestingSupport;
using NUnit.Framework;

namespace Host.Tests
{
    [TestFixture]
    public class QuartzJobSchedulerTests : InteractionContext<QuartzJobScheduler>
    {
        [Test]
        public void Test()
        {
            ClassUnderTest.Run();
        }
    }
}