using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;

namespace Exercises_Appium___Desktop_Testing
{
    public class Tests
    {
        private AppiumLocalService appiumLocalService;
        private WindowsDriver<WindowsElement> driver;
        private const string summatorAppPath = @"WRITE YOUR APP PATH";
        private WindowsElement firstNumberField;
        private WindowsElement secondNumberField;
        private WindowsElement resultField;
        private WindowsElement calculateButton;

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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            firstNumberField = driver.FindElementByAccessibilityId("textBoxFirstNum");
            secondNumberField = driver.FindElementByAccessibilityId("textBoxSecondNum");
            resultField = driver.FindElementByAccessibilityId("textBoxSum");
            calculateButton = driver.FindElementByAccessibilityId("buttonCalc");
        }
        private void CleanFields()
        {
            firstNumberField.Clear();
            secondNumberField.Clear();
        }
        [TestCase("1336","1","1337")]
        [TestCase("400.25","19.75","420.00")]
        [TestCase("0","0","0")]
        [TestCase("-1","0","-1")]
        [TestCase("0.1","-25.6","-25.5")]
        [TestCase("-3","-8","-11")]
        [TestCase("-3","-8","-11")]
        [TestCase("","","error")]
        [TestCase("","46","error")]
        [TestCase("39","","error")]
        [TestCase("-42","","error")]
        [TestCase("","-69.25","error")]
        [TestCase("Valid","-69.25","error")]
        [TestCase("-85","Invalid","error")]
        [TestCase("Valid","Invalid","error")]
        public void Test_SummatorApp_With_ValidAndInvalidData(string firstNumber,string secondNumber,string result)
        {
            CleanFields();    
            firstNumberField.SendKeys(firstNumber);
            secondNumberField.SendKeys(secondNumber);
            calculateButton.Click();
            Assert.That(resultField.Text, Is.EqualTo(result));
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.CloseApp();
        }
    }
}
