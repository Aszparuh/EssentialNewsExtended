using EssentialNewsMvc.Services.Infrastructure.Contracts;
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
            var sanitizeService = new Mock<ISanitizeService>();

            TestDelegate testDelegate = () => new NewsController(mediator, sanitizeService.Object);

            Assert.That(testDelegate, Throws.Exception.With.Message.Contains("mediator"));
        }

        [Test]
        public void ThrowException_When_NullSanitizeService()
        {
            var mediator = new Mock<IMediator>();
            ISanitizeService sanitizeService = null;

            TestDelegate testDelegate = () => new NewsController(mediator.Object, sanitizeService);

            Assert.That(testDelegate, Throws.Exception.With.Message.Contains("sanitize service"));
        }

        [Test]
        public void CreateNewInstance()
        {
            var mockMediator = new Mock<IMediator>();
            var sanitizeService = new Mock<ISanitizeService>();

            var controller = new NewsController(mockMediator.Object, sanitizeService.Object);

            Assert.That(controller, Is.InstanceOf<NewsController>());
        }
    }
}
