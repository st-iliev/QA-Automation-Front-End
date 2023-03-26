using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace VivinoAppTests
{
    public class Tests
    {
        private const string appiumServerUri = "http://127.0.0.1:4723/wd/hub";
        private const string vivinoAppPackage = "vivino.web.app";
        private const string vivinoAppPath = @"E:\QA\QA Automation\Front-End\vivino_8.18.11-8181203.apk";
        private const string vivinoAppStartupActivity = "com.sphinx_solution.activities.SplashActivity";
        private const string vivinoTestEmail = "qaautomation@test.bg";
        private const string vivinoTestPassowrd = "qaautomation";
        private AndroidDriver<AndroidElement> driver;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var appiumOptions = new AppiumOptions() { PlatformName = "Android" };
            appiumOptions.AddAdditionalCapability("app", vivinoAppPath);
            appiumOptions.AddAdditionalCapability("appPackage", vivinoAppPackage);
            appiumOptions.AddAdditionalCapability("appActivity", vivinoAppStartupActivity);
            driver = new AndroidDriver<AndroidElement>(new Uri(appiumServerUri), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }
        [Test, Order(1)]
        public void Test_VivinoApp_SuccessfullyLogIn()
        {
            IList<AndroidElement> descText = driver.FindElementsById("vivino.web.app:id/desc_text");
            if (descText.Count == 1)
            {
                var iHaveAccountLink = driver.FindElementById("vivino.web.app:id/txthaveaccount");
                iHaveAccountLink.Click();
                var emailField = driver.FindElementById("vivino.web.app:id/edtEmail");
                var passwordField = driver.FindElementById("vivino.web.app:id/edtPassword");
                var signIn = driver.FindElementById("vivino.web.app:id/action_signin");
                emailField.SendKeys(vivinoTestEmail);
                passwordField.SendKeys(vivinoTestPassowrd);
                signIn.Click();
            }
            var headerText = driver.FindElementById("vivino.web.app:id/header_text");
            Assert.That(headerText.Text, Is.EqualTo("Trending wines from the Vivino community"));
        }
        [Test, Order(2)]
        public void Test_VivinoApp_SearchForSpecificWine()
        {
            var explorerTab = driver.FindElementById("vivino.web.app:id/wine_explorer_tab");
            explorerTab.Click();
            var searchField = driver.FindElementById("vivino.web.app:id/search_vivino");
            searchField.Click();
            var inputTextField = driver.FindElementById("vivino.web.app:id/editText_input");
            inputTextField.SendKeys("Katarzyna Reserve Red 2006");
            var listView = driver.FindElementById("vivino.web.app:id/listviewWineListActivity");
            var firstResult = listView.FindElementByClassName("android.widget.LinearLayout");
            firstResult.Click();
            var wineName = driver.FindElementById("vivino.web.app:id/wine_name");
            Assert.That(wineName.Text, Is.EqualTo("Reserve Red 2006"));
        }
        [Test, Order(3)]
        public void Test_VivinoApp_CheckInformationForSearchedWine()
        {
            var rating = driver.FindElementById("vivino.web.app:id/rating");
            var tabSummary = driver.FindElementById("vivino.web.app:id/tabs");
            var tabHighlights = tabSummary.FindElementByXPath("//android.widget.TextView[1]");
            tabHighlights.Click();
            var highlightsDescription = driver.FindElementById("vivino.web.app:id/highlight_description");
            Assert.That(highlightsDescription.Text, Is.EqualTo("Among top 1% of all wines in the world"));
            var tabFacts = tabSummary.FindElementByXPath("//android.widget.TextView[2]");
            tabFacts.Click();
            var factsDescription = driver.FindElementById("vivino.web.app:id/wine_fact_text");
            Assert.That(factsDescription.Text, Is.EqualTo("Cabernet Sauvignon,Merlot"));
            Assert.IsTrue(double.Parse(rating.Text) >= 1.00 & double.Parse(rating.Text) <= 5.00);

        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.CloseApp();
            driver.Quit();
        }
    }
}