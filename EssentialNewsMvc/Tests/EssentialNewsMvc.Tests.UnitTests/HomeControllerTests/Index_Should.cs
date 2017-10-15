using EssentialNewsMvc.Web.Caching;
using EssentialNewsMvc.Web.Controllers;
using EssentialNewsMvc.Web.ViewModels.Home;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EssentialNewsMvc.Tests.UnitTests.HomeControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallMediator()
        {
            var newsService = new Mock<INewsCacheService>();
            var sut = new HomeController(newsService.Object);

            var result = sut.Index();

            newsService.Verify(x => x.IndexArticles(), Times.Once());
        }

        [Test]
        public async Task ReturnViewAsync()
        {
            var model = new HomeViewModel();
            var newsService = new Mock<INewsCacheService>();
            newsService.Setup(x => x.IndexArticles()).Returns(Task.FromResult(model));

            var sut = new HomeController(newsService.Object);
            var result = await sut.Index();

            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
        }
    }
}
