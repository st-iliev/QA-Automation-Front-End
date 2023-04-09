using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Tests
{
    public class AddURLTests : BaseTest
    {
        private const string url = "http://qa.bg";
        [Test]
        public void Test_CreateNewURL_With_ValidData()
        {
            addUrlPage.Open();
            addUrlPage.AddURL(url);
            Assert.IsTrue(shortURLPage.IsOpen());
            var allURLs = shortURLPage.GetAddedUrls();
            Assert.Contains(url,allURLs);
            
        }
        [Test]
        public void Test_CreateNewURL_With_InvalidData()
        {
            addUrlPage.Open();
            addUrlPage.AddURL($"invalid{DateTime.Now.Ticks}.bg");
            Assert.True(addUrlPage.errorMsg.Displayed);
            Assert.That(addUrlPage.errorMsg.Text, Is.EqualTo("Invalid URL!"));
        }
    }
}
