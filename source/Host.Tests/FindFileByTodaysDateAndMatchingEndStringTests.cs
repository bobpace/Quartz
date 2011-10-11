using System;
using FubuTestingSupport;
using NUnit.Framework;

namespace Host.Tests
{
    [TestFixture]
    public class FindFileByTodaysDateAndMatchingEndStringTests : InteractionContext<FindFileByTodaysDateAndMatchingEndString>
    {
        protected override void beforeEach()
        {
            Services.Inject(typeof(string), "offer");
            Services.PartialMockTheClassUnderTest();
        }

        [Test]
        public void TestDirectory()
        {
            var result = ClassUnderTest.GetFile(@"c:\test");
            Console.WriteLine(result);
            Assert.IsNotNull(result);
        }
    }
}