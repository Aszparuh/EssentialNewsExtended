using EssentialNewsMvc.Web.Caching;
using EssentialNewsMvc.Web.Controllers;
using EssentialNewsMvc.Web.Features.NewsArticles;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Threading;

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
        public void ReturnView()
        {
            var newsService = new Mock<INewsCacheService>();

            var sut = new HomeController(newsService.Object);
            var result = sut.Index();

            Assert.That(result, Is.Not.Null);
        }
    }
}
