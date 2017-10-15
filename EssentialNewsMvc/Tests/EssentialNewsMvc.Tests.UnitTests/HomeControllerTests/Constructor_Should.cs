using EssentialNewsMvc.Web.Caching;
using EssentialNewsMvc.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace EssentialNewsMvc.Tests.UnitTests.HomeControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowException_When_NullService()
        {
            INewsCacheService service = null;

            TestDelegate testDelegate = () => new HomeController(service);

            Assert.That(testDelegate, Throws.Exception.With.Message.Contains("news cache service"));
        }

        [Test]
        public void CreateNewInstance()
        {
            var service = new Mock<INewsCacheService>();

            var controller = new HomeController(service.Object);

            Assert.That(controller, Is.InstanceOf<HomeController>());
        }
    }
}
