using EssentialNewsMvc.Web.Caching;
using EssentialNewsMvc.Web.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace EssentialNewsMvc.Tests.UnitTests.HomeControllerTests
{
    [TestFixture]
    public class Contact_Should
    {
        [Test]
        public void ReturnView()
        {
            var newsService = new Mock<INewsCacheService>();

            var sut = new HomeController(newsService.Object);
            var result = sut.Contact();

            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
        }
    }
}
