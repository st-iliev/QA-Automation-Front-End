using Exercises_Selenium_Advanced_and_POM.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Exercises_Selenium_Advanced_and_POM.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected HomePage homePage;
        protected ViewStudentsPage viewStudents;
        protected AddStudentPage addStudent;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();
            homePage = new HomePage(driver);
            viewStudents = new ViewStudentsPage(driver);
            addStudent = new AddStudentPage(driver);
        }
        [OneTimeTearDown]
        public void OneTimeTrearDown()
        {
            driver.Quit();
        }
    }
}
