using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Logging;

namespace Prism.Avalonia.Tests.Logging
{
    [TestClass]
    public class TextLoggerFixture
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullTextWriterThrows()
        {
            ILoggerFacade logger = new TextLogger(null);
        }

        [TestMethod]
        public void ShouldWriteToTextWriter()
        {
            TextWriter writer = new StringWriter();
            ILoggerFacade logger = new TextLogger(writer);

            logger.Log("Test", Category.Debug, Priority.Low);
            StringAssert.Contains(writer.ToString(), "Test");
            StringAssert.Contains(writer.ToString(), "DEBUG");
            StringAssert.Contains(writer.ToString(), "Low");
        }

        [TestMethod]
        public void ShouldDisposeWriterOnDispose()
        {
            MockWriter writer = new MockWriter();
            IDisposable logger = new TextLogger(writer);

            Assert.IsFalse(writer.DisposeCalled);
            logger.Dispose();
            Assert.IsTrue(writer.DisposeCalled);
        }
    }

    internal class MockWriter : TextWriter
    {
        public bool DisposeCalled;
        public override Encoding Encoding
        {
            get { throw new NotImplementedException(); }
        }

        protected override void Dispose(bool disposing)
        {
            DisposeCalled = true;
            base.Dispose(disposing);
        }
    }
}