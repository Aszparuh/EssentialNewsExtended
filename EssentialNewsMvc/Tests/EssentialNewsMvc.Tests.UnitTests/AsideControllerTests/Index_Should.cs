using EssentialNewsMvc.Web.Controllers;
using EssentialNewsMvc.Web.Features.NewsArticles;
using EssentialNewsMvc.Web.ViewModels.Partials;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EssentialNewsMvc.Tests.UnitTests.AsideControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallMediator()
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<AsideArticlesQuery>(), It.IsAny<CancellationToken>()));

            var sut = new AsideController(mediator.Object);
            var result = sut.Index();

            mediator.Verify(x => x.Send(It.IsAny<AsideArticlesQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task ReturnViewAsync()
        {
            var model = new AsideViewModel();
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<AsideArticlesQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(model));

            var sut = new AsideController(mediator.Object);
            var result = await sut.Index();

            Assert.That(result, Is.InstanceOf(typeof(PartialViewResult)));
        }
    }
}
