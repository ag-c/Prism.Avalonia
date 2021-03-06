using System;
using Avalonia;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Avalonia.Tests.Mocks;
using Prism.Common;
using Prism.Regions;
using Prism.Regions.Behaviors;

namespace Prism.Avalonia.Tests.Regions.Behaviors
{
    [TestClass]
    public class SyncRegionContextWithHostBehaviorFixture
    {
        [TestMethod]
        public void ShouldForwardRegionContextValueToHostControl()
        {
            MockPresentationRegion region = new MockPresentationRegion();

            SyncRegionContextWithHostBehavior behavior = new SyncRegionContextWithHostBehavior();
            behavior.Region = region;
            Visual mockDependencyObject = new MockDependencyObject();
            behavior.HostControl = mockDependencyObject;

            behavior.Attach();
            Assert.IsNull(region.Context);
            RegionContext.GetObservableContext(mockDependencyObject).Value = "NewValue";

            Assert.AreEqual("NewValue", region.Context);

        }

        [TestMethod]
        public void ShouldUpdateHostControlRegionContextValueWhenContextOfRegionChanges()
        {
            MockPresentationRegion region = new MockPresentationRegion();

            SyncRegionContextWithHostBehavior behavior = new SyncRegionContextWithHostBehavior();
            behavior.Region = region;
            Visual mockDependencyObject = new MockDependencyObject();
            behavior.HostControl = mockDependencyObject;

            ObservableObject<object> observableRegionContext = RegionContext.GetObservableContext(mockDependencyObject);

            behavior.Attach();
            Assert.IsNull(observableRegionContext.Value);
            region.Context = "NewValue";

            Assert.AreEqual("NewValue", observableRegionContext.Value);

        }

        [TestMethod]
        public void ShouldGetInitialValueFromHostAndSetOnRegion()
        {
            MockPresentationRegion region = new MockPresentationRegion();

            SyncRegionContextWithHostBehavior behavior = new SyncRegionContextWithHostBehavior();
            behavior.Region = region;
            Visual mockDependencyObject = new MockDependencyObject();
            behavior.HostControl = mockDependencyObject;

            RegionContext.GetObservableContext(mockDependencyObject).Value = "NewValue";

            Assert.IsNull(region.Context);
            behavior.Attach();
            Assert.AreEqual("NewValue", region.Context);

        }

        [TestMethod]
        public void AttachShouldNotThrowWhenHostControlNull()
        {
            MockPresentationRegion region = new MockPresentationRegion();

            SyncRegionContextWithHostBehavior behavior = new SyncRegionContextWithHostBehavior();
            behavior.Region = region;
            behavior.Attach();
        }

        [TestMethod]
        public void AttachShouldNotThrowWhenHostControlNullAndRegionContextSet()
        {
            MockPresentationRegion region = new MockPresentationRegion();

            SyncRegionContextWithHostBehavior behavior = new SyncRegionContextWithHostBehavior();
            behavior.Region = region;
            behavior.Attach();
            region.Context = "Changed";
        }

        [TestMethod]
        public void ChangingRegionContextObservableObjectValueShouldAlsoChangeRegionContextDependencyProperty()
        {
            MockPresentationRegion region = new MockPresentationRegion();

            SyncRegionContextWithHostBehavior behavior = new SyncRegionContextWithHostBehavior();
            behavior.Region = region;
            Visual hostControl = new MockDependencyObject();
            behavior.HostControl = hostControl;

            behavior.Attach();

            Assert.IsNull(RegionManager.GetRegionContext(hostControl));
            RegionContext.GetObservableContext(hostControl).Value = "NewValue";

            Assert.AreEqual("NewValue", RegionManager.GetRegionContext(hostControl));
        }

        [TestMethod]
        public void AttachShouldChangeRegionContextDependencyProperty()
        {
            MockPresentationRegion region = new MockPresentationRegion();

            SyncRegionContextWithHostBehavior behavior = new SyncRegionContextWithHostBehavior();
            behavior.Region = region;
            Visual hostControl = new MockDependencyObject();
            behavior.HostControl = hostControl;

            RegionContext.GetObservableContext(hostControl).Value = "NewValue";

            Assert.IsNull(RegionManager.GetRegionContext(hostControl));
            behavior.Attach();
            Assert.AreEqual("NewValue", RegionManager.GetRegionContext(hostControl));  
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SettingHostControlAfterAttachThrows()
        {

            SyncRegionContextWithHostBehavior behavior = new SyncRegionContextWithHostBehavior();
            Visual hostControl1 = new MockDependencyObject();
            behavior.HostControl = hostControl1;

            behavior.Attach();
            Visual hostControl2 = new MockDependencyObject();
            behavior.HostControl = hostControl2;
        }
    }
}