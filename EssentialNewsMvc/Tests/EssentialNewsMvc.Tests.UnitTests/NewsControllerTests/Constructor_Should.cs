using EssentialNewsMvc.Web.Controllers;
using MediatR;
using Moq;
using NUnit.Framework;

namespace EssentialNewsMvc.Tests.UnitTests.NewsControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowException_When_NullMediator()
        {
            IMediator mediator = null;

            TestDelegate testDelegate = () => new NewsController(mediator);

            Assert.That(testDelegate, Throws.Exception.With.Message.Contains("mediator"));
        }

        [Test]
        public void CreateNewInstance()
        {
            var mockMediator = new Mock<IMediator>();

            var controller = new NewsController(mockMediator.Object);

            Assert.That(controller, Is.InstanceOf<NewsController>());
        }
    }
}
