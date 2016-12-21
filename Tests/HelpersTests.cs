using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XgagWebsite.Helpers;

namespace Tests
{
    [TestClass]
    public class HelpersTests
    {
        [TestMethod]
        public void TestIsImageUrl()
        {
            Assert.IsTrue(ImagesHelper.IsImageUrl("http://i.memeful.com/media/post/Bdq6LWw_700wa_0.gif"));
            Assert.IsFalse(ImagesHelper.IsImageUrl("http://i.memeful.com/media/post/Bdq6LWw_70"));
        }
    }
}
