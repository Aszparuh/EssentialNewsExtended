using EssentialNewsMvc.Web.Controllers;
using EssentialNewsMvc.Web.Features.NewsArticles;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Threading;

namespace EssentialNewsMvc.Tests.UnitTests.NewsControllerTests
{
    [TestFixture]
    public class Details_Should
    {
        [Test]
        public void CallMediator()
        {
            var Id = 1;
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<AsideArticlesQuery>(), It.IsAny<CancellationToken>()));

            var sut = new NewsController(mediator.Object);
            var result = sut.Details(Id, string.Empty);

            mediator.Verify(x => x.Send(It.Is<NewsDetailsQuery>(y => y.Id == Id), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
