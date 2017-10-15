using EssentialNewsMvc.Web.Controllers;
using EssentialNewsMvc.Web.Features.News;
using EssentialNewsMvc.Web.Features.NewsArticles;
using EssentialNewsMvc.Web.ViewModels.News;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EssentialNewsMvc.Tests.UnitTests.NewsControllerTests
{
    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void CallMediatorOnGet()
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<CategoriesRegionsQuery>(), It.IsAny<CancellationToken>()));

            var sut = new NewsController(mediator.Object);
            var result = sut.Create();

            mediator.Verify(x => x.Send(It.IsAny<CategoriesRegionsQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task Return_View()
        {
            var article = new CreateNewsViewModel();

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<CategoriesRegionsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(article));

            var sut = new NewsController(mediator.Object);
            var result = await sut.Create();

            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
        }

        [Test]
        public void CallMediatorOnPost()
        {

            var file = new Mock<HttpPostedFileBase>();
            var model = new CreateNewsViewModel()
            {
                Title = "Some Title",
                Content = "Some Content",
                IsTop = true,
                NewsCategoryId = 1,
                RegionId = 1,
                SampleContent = "Some sample content",
                Upload = file.Object,
            };
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<CategoriesRegionsQuery>(), It.IsAny<CancellationToken>()));

            var sut = new NewsController(mediator.Object);
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("Name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            sut.ControllerContext = controllerContext.Object;

            var result = sut.Create(model);

            mediator.Verify(x => x.Send(It.IsAny<CreateArticleCommand>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void Return_ViewOnPost()
        {
            var file = new Mock<HttpPostedFileBase>();
            var model = new CreateNewsViewModel()
            {
                Title = "Some Title",
                Content = "Some Content",
                IsTop = true,
                NewsCategoryId = 1,
                RegionId = 1,
                SampleContent = "Some sample content",
                Upload = file.Object,
            };
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<CategoriesRegionsQuery>(), It.IsAny<CancellationToken>()));

            var sut = new NewsController(mediator.Object);
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("Name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            sut.ControllerContext = controllerContext.Object;

            var result = sut.Create(model);
            var redirectResult = (RedirectToRouteResult)result;
            Assert.IsTrue(redirectResult.RouteValues.ContainsKey("action"));
            Assert.IsTrue(redirectResult.RouteValues.ContainsKey("controller"));
            Assert.AreEqual("Index", redirectResult.RouteValues["action"].ToString());
            Assert.AreEqual("Home", redirectResult.RouteValues["controller"].ToString());
        }
    }
}
