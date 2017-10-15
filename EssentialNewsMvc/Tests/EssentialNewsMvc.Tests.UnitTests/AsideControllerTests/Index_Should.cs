using EssentialNewsMvc.Web.Controllers;
using EssentialNewsMvc.Web.Features.NewsArticles;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Threading;

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
    }
}
