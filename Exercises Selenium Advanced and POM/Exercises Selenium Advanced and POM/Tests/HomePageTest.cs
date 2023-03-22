using Exercises_Selenium_Advanced_and_POM.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Exercises_Selenium_Advanced_and_POM.Tests
{
    public class HomePageTest : BaseTest
    {
        
        [Test]
        public void Test_HomePage_Content()
        {
            homePage.Open();
            Assert.That("MVC Example", Is.EqualTo(homePage.GetPageTitle()));
            Assert.That("Students Registry", Is.EqualTo(homePage.GetPageHeader()));
            Assert.GreaterOrEqual(homePage.GetStudentCount(),3);
        }
        [Test]
        public void Test_HomePage_Links()
        {
            homePage.Open();
            homePage.LinkViewStudents.Click();
            Assert.IsTrue(new ViewStudentsPage(driver).IsOpen());

            homePage.Back();
            homePage.LinkAddStudent.Click();
            Assert.IsTrue(new AddStudentPage(driver).IsOpen());

            homePage.Back();
            homePage.LinkHomePage.Click();
            Assert.IsTrue(homePage.IsOpen());
        }
    }
}
