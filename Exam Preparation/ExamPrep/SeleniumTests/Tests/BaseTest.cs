using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Pages;

namespace SeleniumTests.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected AddUrlPage addUrlPage;
        protected ShortURLPage shortURLPage;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();
            addUrlPage = new AddUrlPage(driver);
            shortURLPage = new ShortURLPage(driver);
        }
        [Test]
        public void Test_NavigateTo_NonExistShortURL()
        {
            driver.Navigate().GoToUrl("https://shorturl.nakov.repl.co/go/invalid4587599");
            Assert.True(addUrlPage.errorMsg.Displayed);
            Assert.That(addUrlPage.errorMsg.Text, Is.EqualTo("Cannot navigate to given short URL"));
            Assert.That(driver.Title, Is.EqualTo("Error"));

        }
        [OneTimeTearDown]
        public void OneTimeTrearDown()
        {
            driver.Quit();
        }
    }
}
