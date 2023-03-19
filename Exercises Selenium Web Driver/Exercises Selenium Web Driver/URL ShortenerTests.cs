using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace Exercises_Selenium_Web_Driver
{
    public class URLShortenerTests
    {
        private const string baseURL = "https://shorturl.softuniqa.repl.co/";
        private ChromeDriver driver;
        private IWebElement homePage;
        private IWebElement shortURLPage;
        private IWebElement addURLPage;
        private const string invalidURL = "InvalidURL.bg";
        private bool result = false;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = baseURL;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            homePage = driver.FindElement(By.LinkText("Home"));
            shortURLPage = driver.FindElement(By.LinkText("Short URLs"));
            addURLPage = driver.FindElement(By.LinkText("Add URL"));
        }

        [Test]
        public void Check_ShortURLContainExistURL()
        {
            string expectedTitle = "Short URLs";
            string expectedOriginalURL = "https://nakov.com";
            string expectedShortURL = "http://shorturl.softuniqa.repl.co/go/nak";
            shortURLPage.Click();
            string pageTitle = driver.Title;

            Assert.That(pageTitle, Is.EqualTo(expectedTitle));
            var shortURLCells = driver.FindElements(By.XPath("//table/tbody/tr/td"));

            Assert.That(expectedOriginalURL, Is.EqualTo(shortURLCells[0].Text));
            Assert.That(expectedShortURL, Is.EqualTo(shortURLCells[1].Text));

        }
        [TestCase(invalidURL)]
        [TestCase("")]
        public void Add_InvalidURL(string url)
        {
            string expectedErrorMessage = "Invalid URL!";
           
            addURLPage.Click();

            driver.FindElement(By.Id("url")).SendKeys(invalidURL);
            driver.FindElement(By.XPath("//button[contains(@type,'submit')]")).Click();
            var errorMsg = driver.FindElement(By.XPath("//div[contains(@class,'err')]"));

            Assert.True(errorMsg.Displayed);
            Assert.That(errorMsg.Text, Is.EqualTo(expectedErrorMessage));

        }
        [Test]
        public void Add_ValidURL()
        {          
            string url = @"https:\\itsanew.bg";
            addURLPage.Click();

            driver.FindElement(By.Id("url")).SendKeys(url);
            string shortCode = driver.FindElement(By.Id("code")).Text;
            driver.FindElement(By.XPath("//button[contains(@type,'submit')]")).Click();
            var shortURLCells = driver.FindElements(By.XPath("//table/tbody/tr/td"));
            for (int i = 0; i < shortURLCells.Count; i++) 
            {
               if (shortURLCells[i].Text == url)
                {
                    if (shortURLCells[i + 1].Text.EndsWith(shortCode))
                    {
                        result = true;
                    }
                }
            }
            Assert.True(result);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}