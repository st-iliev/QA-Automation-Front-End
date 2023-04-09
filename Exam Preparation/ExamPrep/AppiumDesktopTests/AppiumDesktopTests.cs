using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumDesktopTests
{
    public class AppiumDesktopTests
    {
        private AppiumLocalService appiumLocalService;
        private WindowsDriver<WindowsElement> driver;
        private const string summatorAppPath = @"YOUR APP PATH";
        private WindowsElement textBoxApiUrl;
        private WindowsElement buttonConnect;
        private WindowsElement buttonAdd;
        private WindowsElement textBoxURL;
        private WindowsElement buttonCreate;
        private WindowsElement myUrl;
        private string apiUrl = "https://shorturl.nakov.repl.co/api";
        private string url = "http://qa.bg";
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            appiumLocalService.Start();
            var appiumOptions = new AppiumOptions()
            {
                PlatformName = "Windows"
            };
            appiumOptions.AddAdditionalCapability("app", summatorAppPath);
            driver = new WindowsDriver<WindowsElement>(appiumLocalService, appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            
        }
        [Test]
        public void CreateNewURL()
        {
            textBoxApiUrl = driver.FindElementByAccessibilityId("textBoxApiUrl");
            buttonConnect = driver.FindElementByAccessibilityId("buttonConnect");
            textBoxApiUrl.SendKeys(apiUrl);
            buttonConnect.Click();
            buttonAdd = driver.FindElementByAccessibilityId("buttonAdd");
            buttonAdd.Click();
            textBoxURL = driver.FindElementByAccessibilityId("textBoxURL");
            textBoxURL.SendKeys(url);
            buttonCreate = driver.FindElementByAccessibilityId("buttonCreate");
            buttonCreate.Click();
            myUrl = driver.FindElementByXPath("//Text[@Name='http://qa.bg']");
            Assert.True(myUrl.Displayed);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.CloseApp();
        }
    }
}