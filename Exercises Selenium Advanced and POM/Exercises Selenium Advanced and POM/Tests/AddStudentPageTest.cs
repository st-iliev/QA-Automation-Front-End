using Exercises_Selenium_Advanced_and_POM.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
namespace Exercises_Selenium_Advanced_and_POM.Tests
{
    public class AddStudentPageTest : BaseTest
    {
        private string name = "Student" + DateTime.Now.Ticks;
        private string email = "email" + DateTime.Now.Ticks + "@email.bg";
        [Test]
        public void Test_TestAddStudentPage_Content()
        {
            addStudent.Open();
            Assert.That("Add Student", Is.EqualTo(addStudent.GetPageTitle()));
            Assert.That("Register New Student", Is.EqualTo(addStudent.GetPageHeader()));

            Assert.That("",Is.EqualTo(addStudent.nameField.Text));
            Assert.That("",Is.EqualTo(addStudent.emailField.Text));
            Assert.That("Add", Is.EqualTo(addStudent.submitButton.Text));
        }
        [Test]
        public void Test_TestAddStudentPage_Links()
        {
            addStudent.Open();
            addStudent.LinkHomePage.Click();
            Assert.IsTrue(new HomePage(driver).IsOpen());

            addStudent.Back();
            addStudent.LinkViewStudents.Click();
            Assert.IsTrue(new ViewStudentsPage(driver).IsOpen());

            addStudent.Back();
            addStudent.LinkAddStudent.Click();
            Assert.IsTrue(addStudent.IsOpen());
        }
        [Test]
        public void Test_TestAddStudentPage_AddValidStudent()
        {
            addStudent.Open();
            addStudent.AddStudent(name, email);
            Assert.IsTrue(viewStudents.IsOpen());
            var studentList = viewStudents.GetRegistredStudents();
            Assert.Contains($"{name} ({email})", studentList);
        }
        [Test]
        public void Test_TestAddStudentPage_AddInvalidStudent()
        {
            addStudent.Open();
            addStudent.AddStudent("", email);
            Assert.IsTrue(addStudent.IsOpen());
            Assert.IsTrue(addStudent.errorMsg.Text.Contains("Cannot add student"));

        }
    }
}
