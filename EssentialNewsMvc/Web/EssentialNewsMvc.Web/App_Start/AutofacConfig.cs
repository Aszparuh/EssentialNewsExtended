using Autofac;
using Autofac.Features.Variance;
using Autofac.Integration.Mvc;
using EssentialNewsMvc.Data;
using EssentialNewsMvc.Services.Infrastructure.Contracts;
using EssentialNewsMvc.Web.ViewModels.Home;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;

namespace EssentialNewsMvc.Web.App_Start
{
    public static class AutofacConfig
    {
        public static IContainer Container
        {
            get;
            private set;
        }

        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.Register(x => new ApplicationDbContext())
                .As<DbContext>()
                .InstancePerRequest();

            builder.Register(x => new ApplicationDbContext())
                .As<ApplicationDbContext>()
                .InstancePerRequest();

            var anotherServicesAssembly = Assembly.GetAssembly(typeof(IImageProcessService));
            builder.RegisterAssemblyTypes(anotherServicesAssembly).AsImplementedInterfaces();

            // enables contravariant Resolve() for interfaces with single contravariant ("in") arg
            builder
              .RegisterSource(new ContravariantRegistrationSource());

            // mediator itself
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            // request handlers
            builder
              .Register<SingleInstanceFactory>(ctx => {
                  var c = ctx.Resolve<IComponentContext>();
                  return t => { object o; return c.TryResolve(t, out o) ? o : null; };
              })
              .InstancePerLifetimeScope();

            // notification handlers
            builder
              .Register<MultiInstanceFactory>(ctx => {
                  var c = ctx.Resolve<IComponentContext>();
                  return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
              })
              .InstancePerLifetimeScope();

            // finally register our custom code (individually, or via assembly scanning)
            // - requests & handlers as transient, i.e. InstancePerDependency()
            // - pre/post-processors as scoped/per-request, i.e. InstancePerLifetimeScope()
            // - behaviors as transient, i.e. InstancePerDependency()
            builder.RegisterAssemblyTypes(typeof(NewsArticleIndexViewModel).GetTypeInfo().Assembly).AsImplementedInterfaces(); // via assembly scan
            //builder.RegisterType<MyHandler>().AsImplementedInterfaces().InstancePerDependency();          // or individually
        }
    }
}