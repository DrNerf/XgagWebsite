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

        [TestMethod]
        public void TestYouTubeHelper()
        {
            var testUrl = "https://www.youtube.com/watch?v=4xlIluAImts";
            var expectedResult = "<iframe title='YouTube video player' class='post-image' height='400' src='http://www.youtube.com/embed/4xlIluAImts' frameborder='0' allowfullscreen='1'></iframe>";
            string resultUrl = string.Empty;
            var result = YouTubeHelper.TryGetEmbedHtml(testUrl, out resultUrl);

            Assert.IsTrue(result);
            Assert.AreEqual(resultUrl, expectedResult);
        }
    }
}
