using EssentialNewsMvc.Web.Controllers;
using EssentialNewsMvc.Web.Features.NewsArticles;
using EssentialNewsMvc.Web.ViewModels.News;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

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

        [Test]
        public async Task Return_NotFound_IfArticleTitleIsNotTheSameAsync()
        {
            var article = new DetailsViewModel()
            {
                Title = "Some Title"
            };
            var Id = 1;
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<NewsDetailsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(article));

            var sut = new NewsController(mediator.Object);
            var result = await sut.Details(Id, string.Empty);

            Assert.That(result, Is.InstanceOf(typeof(HttpNotFoundResult)));
        }

        [Test]
        public async Task Return_View()
        {
            var article = new DetailsViewModel()
            {
                Title = string.Empty
            };
            var Id = 1;
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<NewsDetailsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(article));

            var sut = new NewsController(mediator.Object);
            var result = await sut.Details(Id, string.Empty);

            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
        }
    }
}
