using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;
using System;


namespace EssentialNewsMvc.Tests.IntegrationTests
{
    public class SimpleLifetimeScopeProvider : ILifetimeScopeProvider
    {
        private readonly IContainer container;
        private ILifetimeScope scope;

        public SimpleLifetimeScopeProvider(IContainer container)
        {
            this.container = container;
        }

        public ILifetimeScope ApplicationContainer
        {
            get { return this.container; }
        }

        public void EndLifetimeScope()
        {
            if (this.scope != null)
            {
                this.scope.Dispose();
                this.scope = null;
            }
        }

        public ILifetimeScope GetLifetimeScope(Action<ContainerBuilder> configurationAction)
        {
            if (this.scope == null)
            {
                this.scope = (configurationAction == null)
                       ? this.ApplicationContainer.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag)
                       : this.ApplicationContainer.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag, configurationAction);
            }

            return this.scope;
        }
    }
}
