﻿using System;
using Avalonia;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.IocContainer.Avalonia.Tests.Support;
using Prism.Modularity;

namespace Prism.Autofac.Avalonia.Tests
{
    [TestClass]
    public class AutofacBootstrapperNullModuleCatalogFixture : BootstrapperFixtureBase
    {
        [TestMethod]
        public void NullModuleCatalogThrowsOnDefaultModuleInitialization()
        {
            var bootstrapper = new NullModuleCatalogBootstrapper();

            AssertExceptionThrownOnRun(bootstrapper, typeof(InvalidOperationException), "IModuleCatalog");
        }

        private class NullModuleCatalogBootstrapper : AutofacBootstrapper
        {
            protected override IModuleCatalog CreateModuleCatalog()
            {
                return null;
            }

            protected override IAvaloniaObject CreateShell()
            {
                throw new NotImplementedException();
            }

            protected override void InitializeShell()
            {
                throw new NotImplementedException();
            }
        }
    }
}
