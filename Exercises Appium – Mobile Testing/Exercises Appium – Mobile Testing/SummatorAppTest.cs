using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace Exercises_Appium___Mobile_Testing
{
    public class Tests
    {
        private const string appiumServerUri = "http://127.0.0.1:4723/wd/hub";
        private string summatorAppPath = @"WRITE YOUR APP PATH";
        private AndroidDriver<AndroidElement> driver;
        private AndroidElement firstNumberField;
        private AndroidElement secondNumberField;
        private AndroidElement resultField;
        private AndroidElement calculateButton;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new AppiumOptions()
            {
                PlatformName = "Android"
            };
            options.AddAdditionalCapability("app", summatorAppPath);
            driver = new AndroidDriver<AndroidElement>(new Uri(appiumServerUri), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            firstNumberField = driver.FindElementById("com.example.androidappsummator:id/editText1");
            secondNumberField = driver.FindElementById("com.example.androidappsummator:id/editText2");
            resultField = driver.FindElementById("com.example.androidappsummator:id/editTextSum");
            calculateButton = driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");
        }
        private void ClearFields()
        {
            firstNumberField.Clear();
            secondNumberField.Clear();
        }
        [TestCase("14","15","29")]
        [TestCase("-15","2","-13")]
        [TestCase("6","-3","3")]
        [TestCase("-7","-8.5","-15.5")]
        [TestCase("0.5","1.7","2.2")]
        [TestCase("","1.7","error")]
        [TestCase("4","","error")]
        [TestCase("","","error")]
        public void Test_SummatorApp_With_ValidAndInvalidData(string firstNumber , string secondNumber , string result)
        {
            ClearFields();
            firstNumberField.SendKeys(firstNumber);
            secondNumberField.SendKeys(secondNumber);
            calculateButton.Click();
            Assert.That(resultField.Text,Is.EqualTo(result));
        }
        [OneTimeTearDown]
        public void Test1()
        {
            driver.CloseApp();
        }
    }
}
