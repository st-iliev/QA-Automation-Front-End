using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Exercises_Selenium_Advanced_and_POM.PageObjects
{
    public class ViewStudentsPage : BasePage
    {
        public ViewStudentsPage(IWebDriver driver) : base(driver){}
        public ReadOnlyCollection<IWebElement> registredStudents => driver.FindElements(By.XPath("//body/ul/li"));
        public string[] GetRegistredStudents() => registredStudents.Select(x => x.Text).ToArray();
        public override string PageUrl => "https://studentregistry.softuniqa.repl.co/students";
       
    }
}
