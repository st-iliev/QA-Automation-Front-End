using Exercises_Selenium_Advanced_and_POM.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Exercises_Selenium_Advanced_and_POM.Tests
{
    public class ViewStudentPageTest : BaseTest
    {
        [Test]
        public void Test_ViewStudentsPage_Content()
        {
            viewStudents.Open();
            Assert.That("Students", Is.EqualTo(viewStudents.GetPageTitle()));
            Assert.That("Registered Students", Is.EqualTo(viewStudents.GetPageHeader()));

            string[] studentsList = viewStudents.GetRegistredStudents();

            foreach (var student in studentsList)
            {
                Assert.That(student.Contains("("));
                Assert.That(student.EndsWith(")"));
            }      
        }
        [Test]
        public void Test_ViewStudentsPage_Links()
        {
            viewStudents.Open();
            viewStudents.LinkHomePage.Click();
            Assert.IsTrue(new HomePage(driver).IsOpen());

            viewStudents.Back();
            viewStudents.LinkAddStudent.Click();
            Assert.IsTrue(new AddStudentPage(driver).IsOpen());

            viewStudents.Back();
            viewStudents.LinkViewStudents.Click();
            Assert.IsTrue(viewStudents.IsOpen());
        }

    }
}
