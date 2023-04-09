using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumMobileTests
{
    public class AppiumMobileTests
    {
        private const string appiumServerUri = "http://127.0.0.1:4723/wd/hub";
        private string githubApp = @"YOUR APP PATH";
        private AndroidDriver<AndroidElement> driver;
        private AndroidElement searchField;
        private AndroidElement result;
        private AndroidElement developer;
        private AndroidElement name;
        
        
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new AppiumOptions()
            {
                PlatformName = "Android"
            };
            options.AddAdditionalCapability("app", githubApp);
            driver = new AndroidDriver<AndroidElement>(new Uri(appiumServerUri), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);    
        }
        [Test]
        public void Search_For_Develepor()
        {
            searchField = driver.FindElementById("com.android.example.github:id/input");
            searchField.SendKeys("Selenium");
            driver.PressKeyCode(AndroidKeyCode.Enter);
            result = driver.FindElementByXPath("//android.view.ViewGroup/android.widget.TextView[2]");
            result.Click();
            developer = driver.FindElementByXPath("//android.widget.FrameLayout[2]/android.view.ViewGroup");
            developer.Click();
            name = driver.FindElementByAccessibilityId("user name");
            Assert.That(name.Text, Is.EqualTo("Alexei Barantsev"));
        }
        [OneTimeTearDown]
        public void Test1()
        {
            driver.CloseApp();
        }
    }
}