
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumDesktopTests
{
    public class AppiumDesktopTests
    {

        private const string appiumServerUri = "http://127.0.0.1:4723/wd/hub";
        private WindowsDriver<WindowsElement> driver;
        private WindowsDriver<WindowsElement> desktopDriver;
        private const string contactBookPath = @"WRITE YOUR APP PATH";
        private const string url = ": https://contactbook.nakov.repl.co/api";
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var appiumOptions = new AppiumOptions()
            {
                PlatformName = "Windows"
            };
            appiumOptions.AddAdditionalCapability("app", contactBookPath);
            driver = new WindowsDriver<WindowsElement>(new Uri(appiumServerUri), appiumOptions);
            var appiumOptionsDesktop = new AppiumOptions()
            {
                PlatformName = "Windows"
            };
            appiumOptionsDesktop.AddAdditionalCapability("app", "Root");
            desktopDriver = new WindowsDriver<WindowsElement>(new Uri(appiumServerUri), appiumOptionsDesktop);
        }
        [Test]
        public void Test_SearchFor_ValidContact()
        {
            var searchField = driver.FindElementByAccessibilityId("textBoxApiUrl");
            searchField.SendKeys(url);
            var buttonConnect = driver.FindElementByAccessibilityId("buttonConnect");
            buttonConnect.Click();
            var textBoxSearch = desktopDriver.FindElementByAccessibilityId("textBoxSearch");
            textBoxSearch.SendKeys("steve");
            var buttonSearch = desktopDriver.FindElementByAccessibilityId("buttonSearch");
            buttonSearch.Click();
            var firstName = desktopDriver.FindElementByName("FirstName Row 0, Not sorted.").Text;
            var lastName = desktopDriver.FindElementByName("LastName Row 0, Not sorted.").Text;

            Assert.That(firstName, Is.EqualTo("Steve"));
            Assert.That(lastName, Is.EqualTo("Jobs"));
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.CloseApp();
            driver.Quit();
        }

    }
}