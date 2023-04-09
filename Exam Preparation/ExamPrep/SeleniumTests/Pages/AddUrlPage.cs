using OpenQA.Selenium;

namespace SeleniumTests.Pages
{
    public class AddUrlPage : BasePage
    {
        public AddUrlPage(IWebDriver driver) : base(driver)
        {
        }
        public IWebElement addURLField => driver.FindElement(By.Id("url"));
        public IWebElement shortCodeField => driver.FindElement(By.Id("code"));
        public IWebElement submitButton => driver.FindElement(By.XPath("//button[@type='submit']"));
        public IWebElement errorMsg => driver.FindElement(By.XPath("//*[@class='err']"));
        public override string PageUrl => "https://shorturl.nakov.repl.co/add-url";
        public void AddURL(string url)
        {
            addURLField.SendKeys(url);
            submitButton.Click();
        }
    }
}
