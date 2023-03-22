using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises_Selenium_Advanced_and_POM.PageObjects
{
    public class AddStudentPage : BasePage
    {
        public AddStudentPage(IWebDriver driver) : base(driver) { }
        public override string PageUrl => "https://studentregistry.softuniqa.repl.co/add-student";
        public IWebElement nameField => driver.FindElement(By.Id("name"));
        public IWebElement emailField => driver.FindElement(By.Id("email"));
        public IWebElement submitButton => driver.FindElement(By.XPath("//button[@type='submit']"));
        public IWebElement errorMsg => driver.FindElement(By.XPath("//body/div"));
       
        public void AddStudent(string name, string email)
        {
            nameField.SendKeys(name);
            emailField.SendKeys(email);
            submitButton.Click();
        }
        public string GetErrorMsg() => errorMsg.Text;
    }
}
