using EssentialNewsMvc.Web.Caching;
using EssentialNewsMvc.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace EssentialNewsMvc.Tests.UnitTests.HomeControllerTests
{
    [TestFixture]
    public class About_Should
    {
        [Test]
        public void ReturnView()
        {
            var newsService = new Mock<INewsCacheService>();

            var sut = new HomeController(newsService.Object);
            var result = sut.About();

            Assert.That(result, Is.Not.Null);
        }
    }
}
