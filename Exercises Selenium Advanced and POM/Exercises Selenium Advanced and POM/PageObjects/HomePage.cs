using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises_Selenium_Advanced_and_POM.PageObjects
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver){}
        public override string PageUrl => "https://studentregistry.softuniqa.repl.co/";
        public IWebElement studentCount => driver.FindElement(By.XPath("//p/b"));
        public int GetStudentCount() => int.Parse(studentCount.Text);

    }
}
