using EssentialNewsMvc.Web.Controllers;
using MediatR;
using NUnit.Framework;

namespace EssentialNewsMvc.Tests.UnitTests.AsideControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowException_When_NullMediator()
        {
            IMediator mediator = null;

            TestDelegate testDelegate = () => new AsideController(mediator);

            Assert.That(testDelegate, Throws.Exception.With.Message.Contains("mediator"));
        }
    }
}
