

using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace AppiumMobileTests
{
    public class AppiumMobileTests
    {
        private const string appiumServerUri = "http://127.0.0.1:4723/wd/hub";
        private string contactBookPath = @"WRITE YOUR APP PATH";
        private const string url = ": https://contactbook.nakov.repl.co/api";
        private AndroidDriver<AndroidElement> driver;
       
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new AppiumOptions()
            {
                PlatformName = "Android"
            };
            options.AddAdditionalCapability("app", contactBookPath);
            driver = new AndroidDriver<AndroidElement>(new Uri(appiumServerUri), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        [Test]
        public void Test_SearchFor_ExistContact()
        {
            var searchField = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
            searchField.Clear();
            searchField.SendKeys(url);
            var buttonConnect = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            buttonConnect.Click();
            var searchTextField = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            searchTextField.SendKeys("steve");
            var searchButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searchButton.Click();
            var contacts = driver.FindElementsById("contactbook.androidclient:id/recyclerViewContacts");
            var firstContact = contacts.First();
            var contactFirstName = firstContact.FindElementById("contactbook.androidclient:id/textViewFirstName");
            var contactLastName = firstContact.FindElementById("contactbook.androidclient:id/textViewLastName");

            Assert.That(contactFirstName.Text, Is.EqualTo("Steve"));
            Assert.That(contactLastName.Text, Is.EqualTo("Jobs"));
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}