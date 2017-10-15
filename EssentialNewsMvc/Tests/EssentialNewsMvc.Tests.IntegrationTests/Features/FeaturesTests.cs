using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using EssentialNewsMvc.Data;
using EssentialNewsMvc.Data.Models;
using EssentialNewsMvc.Infrastructure.Mappings;
using EssentialNewsMvc.Web.App_Start;
using EssentialNewsMvc.Web.Features.NewsArticles;
using EssentialNewsMvc.Web.ViewModels.Account;
using EssentialNewsMvc.Web.ViewModels.Partials;
using MediatR;
using NUnit.Framework;
using Respawn;
using System.Configuration;
using System.Reflection;
using System.Web.Mvc;

namespace EssentialNewsMvc.Tests.IntegrationTests.Features
{
    [TestFixture]
    public class FeaturesTests
    {
        private static IContainer container;
        private IDependencyResolver originalResolver = null;
        private ILifetimeScopeProvider scopeProvider = null;
        private static Checkpoint checkpoint = new Checkpoint();

        [SetUp]
        public void Init()
        {
            Assembly asm = typeof(LoginViewModel).Assembly;
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(asm);
            AutofacConfig.RegisterAutofac();
            container = AutofacConfig.Container;
            this.scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, this.scopeProvider);
            this.originalResolver = DependencyResolver.Current;
            DependencyResolver.SetResolver(resolver);
        }

        [TearDown]
        public void Cleanup()
        {
            this.scopeProvider.EndLifetimeScope();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            checkpoint.Reset(connectionString);
        }

        [Test]
        public void AsideArticleHandlerShould_GetArticles()
        {
            
            var handler = DependencyResolver.Current.GetService<IRequestHandler<AsideArticlesQuery, AsideViewModel>>();
            var dbContext = DependencyResolver.Current.GetService<ApplicationDbContext>();
            var article = new NewsArticle()
            {
                Title = "SomeTitle",
                Content = "SomeContent",
                SampleContent = "SomeSampleContent"
            };
            dbContext.NewsArticles.Add(article);
            dbContext.SaveChanges();

            var result = handler.Handle(new AsideArticlesQuery());

            Assert.That(result, Is.InstanceOf(typeof(AsideViewModel)));
        }
    }
}
