using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Internal;

namespace Selenium_Web_Driver

{
    public class SeleniumTest
    {
        private IWebDriver driver;
        private string baseURL = "https://www.wikipedia.org/";
        [SetUp]
        public void Setup()
        {          
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(baseURL);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test_SearchForQAinWikipedia()
        {
            var searchBar = driver.FindElement(By.Id("searchInput"));
            var searchButton = driver.FindElement(By.XPath("//*[@id='search-form']/fieldset/button"));
            searchBar.SendKeys("QA");
            searchButton.Click();
            Assert.That("https://en.wikipedia.org/wiki/QA", Is.EqualTo(driver.Url));
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}