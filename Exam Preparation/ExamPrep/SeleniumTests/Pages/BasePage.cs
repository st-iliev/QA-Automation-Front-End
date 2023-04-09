using OpenQA.Selenium;

namespace SeleniumTests.Pages
{
    public class BasePage
    {
        
        protected readonly IWebDriver driver;
        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }
        public virtual string PageUrl { get; }
        public IWebElement LinkHomePage => driver.FindElement(By.XPath("//a[contains(., 'Home')]"));
        public IWebElement LinkShortURLs => driver.FindElement(By.XPath("//a[contains(., 'Short URLs')]"));
        public IWebElement LinkAddURL => driver.FindElement(By.XPath("//a[contains(., 'Add URL')]"));
        public IWebElement pageHeading => driver.FindElement(By.XPath("//body/h1"));
        public void Open() => driver.Navigate().GoToUrl(PageUrl);
        public bool IsOpen() => driver.Url == PageUrl;
        public void Back() => driver.Navigate().Back();
        public string GetPageTitle() => driver.Title;
        public string GetPageHeader() => pageHeading.Text;
    }
}
