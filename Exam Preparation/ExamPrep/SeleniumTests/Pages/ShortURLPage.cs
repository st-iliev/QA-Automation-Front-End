using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    public class ShortURLPage : BasePage
    {
        
        public ShortURLPage(IWebDriver driver) : base(driver)
        {
        }
        public IList<IWebElement> tableRowsHeader => driver.FindElements(By.XPath("//table/thead/tr/th"));
        public IList<IWebElement> tableRows => driver.FindElements(By.XPath("//table/tbody/tr"));
        public IList<IWebElement> tableCells => driver.FindElements(By.XPath("//table/tbody/tr/td"));
        public IWebElement firstRowUrlVisitCount => driver.FindElements(By.XPath("//table/tbody/tr[1]/td")).Last();
        public IWebElement firstRowUrlShortCode => driver.FindElement(By.XPath("//table/tbody/tr[1]/td[2]"));
        public List<string> GetAddedUrls() => tableCells.Select(x => x.Text).ToList();
        public override string PageUrl => "https://shorturl.nakov.repl.co/urls";


    }
}
