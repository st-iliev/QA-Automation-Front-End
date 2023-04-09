using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumTests.Pages;
using OpenQA.Selenium.Chrome;
namespace SeleniumTests.Tests
{
    public class ShortURLTests : BaseTest
    {
        [Test]
        public void Test_NameOfFirstHeaderRow()
        {
            shortURLPage.Open();
            Assert.That(shortURLPage.tableRowsHeader[0].Text, Is.EqualTo("Original URL"));
        }
        [Test]
        public void Test_VisitExistShortURL()
        {
            shortURLPage.Open();
            int shortURLCountBefore = int.Parse(shortURLPage.firstRowUrlVisitCount.Text);
            shortURLPage.firstRowUrlShortCode.Click();
            Thread.Sleep(500);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            Assert.True(shortURLPage.IsOpen());
            int shortURLCountAfter = int.Parse(shortURLPage.firstRowUrlVisitCount.Text);
            Assert.Greater(shortURLCountAfter, shortURLCountBefore);

        }

    }
}
