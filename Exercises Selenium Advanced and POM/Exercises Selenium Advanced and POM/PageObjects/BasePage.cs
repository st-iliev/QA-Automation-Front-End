using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises_Selenium_Advanced_and_POM.PageObjects
{
    public class BasePage
    {
        protected readonly IWebDriver driver;
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        public virtual string PageUrl {get;}
        public IWebElement LinkHomePage => driver.FindElement(By.XPath("//a[contains(., 'Home')]"));
        public IWebElement LinkViewStudents => driver.FindElement(By.XPath("//a[contains(., 'View Students')]"));
        public IWebElement LinkAddStudent => driver.FindElement(By.XPath("//a[contains(., 'Add Student')]"));
        public IWebElement pageHeading => driver.FindElement(By.XPath("//body/h1"));
        public void Open() => driver.Navigate().GoToUrl(PageUrl);
        public  bool IsOpen() => driver.Url == PageUrl;
        public void Back() => driver.Navigate().Back();
        public  string GetPageTitle() => driver.Title;
        public  string GetPageHeader() => pageHeading.Text;
    }
}
