

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SeleniumTests
{
    public class SeleniumTests
    {
        private IWebDriver driver;
        private const string baseURL = "https://contactbook.nakov.repl.co";
        private IWebElement viewContactButton;
        private IWebElement contactSearchButton;
        private IWebElement createContactButton;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();
            driver.Url = baseURL;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            viewContactButton = driver.FindElement(By.LinkText("Contacts"));
            contactSearchButton = driver.FindElement(By.LinkText("Search"));
            createContactButton = driver.FindElement(By.LinkText("Create"));

        }
        [Test]
        public void Test_CheckFor_FirstContact()
        {
           
            viewContactButton.Click();
            var firstContactFname = driver.FindElement(By.XPath("//*[@id=\"contact1\"]/tbody/tr[1]/td"));
            var firstContactLname = driver.FindElement(By.XPath("//*[@id=\"contact1\"]/tbody/tr[2]/td"));
            Assert.That(firstContactFname.Text, Is.EqualTo("Steve"));
            Assert.That(firstContactLname.Text, Is.EqualTo("Jobs"));
        }
        [Test]
        public void Test_SearchFor_ExistContact()
        {
            
            contactSearchButton.Click();
            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys("albert");
            var searchButton = driver.FindElement(By.Id("search"));
            searchButton.Click();

            var firstContactFname = driver.FindElement(By.XPath("//*[@id=\"contact3\"]/tbody/tr[1]/td"));
            var firstContactLname = driver.FindElement(By.XPath("//*[@id=\"contact3\"]/tbody/tr[2]/td"));
            Assert.That(firstContactFname.Text, Is.EqualTo("Albert"));
            Assert.That(firstContactLname.Text, Is.EqualTo("Einstein"));
        }
        [Test]
        public void Test_SearchFor_InvalidContact()
        {
            string invalidContactName = $"missing{DateTime.Now.Ticks}";
            contactSearchButton.Click();
            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys(invalidContactName);
            var searchButton = driver.FindElement(By.Id("search"));
            searchButton.Click();
            var result = driver.FindElement(By.Id("searchResult"));
            Assert.True(result.Displayed);
            Assert.That(result.Text,Is.EqualTo("No contacts found."));
        }
        [Test]
        public void Test_CreateNew_InvalidContact()
        {
            createContactButton.Click();
            var firstNameField = driver.FindElement(By.Id("firstName"));
            firstNameField.SendKeys("");
            var lastNameField = driver.FindElement(By.Id("lastName"));
            lastNameField.SendKeys("James");
            var emailField = driver.FindElement(By.Id("email"));
            emailField.SendKeys("qaExam@test.bg");
            var phoneField = driver.FindElement(By.Id("phone"));
            phoneField.SendKeys("+359886951457");
            var commentsField = driver.FindElement(By.Id("comments"));
            commentsField.SendKeys("The King");
            var createButton = driver.FindElement(By.Id("create"));
            createButton.Click();
            var errorMsg = driver.FindElement(By.XPath("//*[@class='err']"));

            Assert.True(errorMsg.Displayed);
            Assert.That(errorMsg.Text, Is.EqualTo("Error: First name cannot be empty!"));
        }
        [Test]
        public void Test_CreateNew_ValidContact()
        {
            createContactButton.Click();
            var firstNameField = driver.FindElement(By.Id("firstName"));
            firstNameField.SendKeys("Lebron");
            var lastNameField = driver.FindElement(By.Id("lastName"));
            lastNameField.SendKeys("James");
            var emailField = driver.FindElement(By.Id("email"));
            emailField.SendKeys("qaExam@test.bg");
            var createButton = driver.FindElement(By.Id("create"));
            createButton.Click();

            var contacts = driver.FindElements(By.XPath("//div[@class='contacts-grid']/a"));
            var lastContact = contacts.Last();
            var firstNameLabel = lastContact.FindElement(By.ClassName("fname")).FindElement(By.TagName("td")).Text;
            var lastNameLabel = lastContact.FindElement(By.ClassName("lname")).FindElement(By.TagName("td")).Text;

            Assert.That(firstNameLabel,Is.EqualTo("Lebron"));
            Assert.That(lastNameLabel, Is.EqualTo("James"));
        }
        [OneTimeTearDown]
        public void OneTimeTrearDown()
        {
            driver.Quit();
        }
    }
}