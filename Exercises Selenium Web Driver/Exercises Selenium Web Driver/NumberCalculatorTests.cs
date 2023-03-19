using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Exercises_Selenium_Web_Driver
{
    public class NumberCalculatorTests
    {
        private const string baseURL = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";
        private IWebDriver driver;
        private IWebElement firstNumberField;
        private IWebElement secondNumberField;
        private SelectElement operationMenu;
        private IWebElement calculateButton;
        private IWebElement resetButton;
        private IWebElement resultField;
        

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = baseURL;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            firstNumberField = driver.FindElement(By.Id("number1"));
            secondNumberField = driver.FindElement(By.Id("number2"));
            operationMenu = new SelectElement(driver.FindElement(By.Id("operation")));
            calculateButton = driver.FindElement(By.Id("calcButton"));
            resetButton = driver.FindElement(By.Id("resetButton"));
            resultField = driver.FindElement(By.Id("result"));
        }
        [TestCase("1338", "-","1", "Result: 1337")]
        [TestCase("8", "+", "1", "Result: 9")]
        [TestCase("50", "*", "16", "Result: 800")]
        [TestCase("1337", "/", "5", "Result: 267.4")]
        [TestCase("4.6", "+", "2.8", "Result: 7.4")]
        [TestCase("7.1", "-", "3.9", "Result: 3.2")]
        [TestCase("9.2", "*", "4.7", "Result: 43.24")]
        [TestCase("58.32", "/", "69.74", "Result: 0.836248924577")]
        [TestCase("tryIt", "+", "123", "Result: invalid input")]
        [TestCase("try101", "-", "254", "Result: invalid input")]
        [TestCase("invalid", "*", "id", "Result: invalid input")]
        [TestCase("invalid11", "/", "valid33", "Result: invalid input")]
        [TestCase("", "+", "", "Result: invalid input")]
        [TestCase("", "-", "33", "Result: invalid input")]
        [TestCase("74982", "*", "", "Result: invalid input")]
        [TestCase("thistime", "/", "", "Result: invalid input")]
        public void TestWebCaclculator_With_ValidAndInvalidData(string firstNumber ,  string operation,string secondNumber , string result)
        {
            firstNumberField.SendKeys(firstNumber);
            operationMenu.SelectByValue(operation);
            secondNumberField.SendKeys(secondNumber);
            calculateButton.Click();
            Assert.That(resultField.Text, Is.EqualTo(result));
            resetButton.Click();
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
