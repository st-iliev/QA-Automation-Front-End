using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace Selenium_Web_Driver
{
    public class SummatorTest
    {
        private IWebDriver driver;
        private string baseURL = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";
        private IWebElement firstNumberField;
        private IWebElement secondNumberField;
        private SelectElement operationDropMenu;
        private IWebElement calculateButton;
        private IWebElement resetButton;
        private IWebElement resultField;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(baseURL);
            driver.Manage().Window.Maximize();
            firstNumberField = driver.FindElement(By.Id("number1"));
            secondNumberField = driver.FindElement(By.Id("number2"));
            operationDropMenu = new SelectElement(driver.FindElement(By.Id("operation")));
            calculateButton = driver.FindElement(By.Id("calcButton"));
            resetButton = driver.FindElement(By.Id("resetButton"));
            resultField = driver.FindElement(By.Id("result"));
        }
        [Test]
        public void Test_SumTwoValidNumbers()
        {
            firstNumberField.SendKeys("1330");
            operationDropMenu.SelectByValue("+");
            secondNumberField.SendKeys("7");
            calculateButton.Click();

            Assert.That(resultField.Text, Is.EqualTo("Result: 1337"));
        }
        [Test]
        public void Test_SumTwoInvalidNumbers()
        {
            firstNumberField.SendKeys("LoL");
            operationDropMenu.SelectByValue("+");
            secondNumberField.SendKeys("zZ");
            calculateButton.Click();

            Assert.That(resultField.Text, Is.EqualTo("Result: invalid input"));
        }
        [Test]
        public void Test_ResetButton()
        {
            firstNumberField.SendKeys("57");
            operationDropMenu.SelectByValue("+");
            secondNumberField.SendKeys("16");
            calculateButton.Click();

            Assert.That(firstNumberField.GetAttribute("value"),Is.Not.Empty);
            Assert.IsNotEmpty(operationDropMenu.SelectedOption.Text);
            Assert.That(secondNumberField.GetAttribute("value"), Is.Not.Empty);
            Assert.IsNotEmpty(resultField.Text);
            
            resetButton.Click();

            Assert.That(firstNumberField.Text, Is.Empty);
            Assert.That(operationDropMenu.SelectedOption.Text,Is.EqualTo("-- select an operation --"));
            Assert.That(secondNumberField.Text, Is.Empty);
            Assert.False(resultField.Displayed);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
