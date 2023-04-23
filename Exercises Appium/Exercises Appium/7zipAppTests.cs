using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Exercises_Appium
{
    public class Tests
    {
        private const string appiumServerUri = "http://127.0.0.1:4723/wd/hub";
        private string sevenZipApp = @"WRITE YOUR APP PATH";
        private WindowsDriver<WindowsElement> driver;
        private WindowsDriver<WindowsElement> desktopDriver;
        private string workDir;
        private string archiveFileName;
        private string archiveName;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var appiumOptions = new AppiumOptions()
            {
                PlatformName = appiumServerUri
            };
            appiumOptions.AddAdditionalCapability("app", sevenZipApp);
            driver = new WindowsDriver<WindowsElement>(new Uri(appiumServerUri),appiumOptions);
            workDir = @"WRITE YOUR WORK DIRECTORY";
            if (Directory.Exists(workDir))
            {
                Directory.Delete(workDir, true);
            }
            Directory.CreateDirectory(workDir);
            var appiumOptionsDesktop = new AppiumOptions()
            {
                PlatformName = "Windows"
            };
            appiumOptionsDesktop.AddAdditionalCapability("app", "Root");
            desktopDriver = new WindowsDriver<WindowsElement>(new Uri(appiumServerUri), appiumOptionsDesktop);
        }
        
        [Test,Order(1)]
        public void Test_7ZipApp_Create_7zipArchive()
        {
            archiveFileName = workDir + DateTime.Now.Ticks +".7z";
            archiveName = archiveFileName.Split(@"\")[6];
            var locationFolderField = driver.FindElementByAccessibilityId("1003");
            locationFolderField.SendKeys(@"APP PATH" + Keys.Enter);
            var filesList = driver.FindElementByClassName("SysListView32");
            filesList.SendKeys(Keys.Control + 'a');
            var addButton = driver.FindElementByName("Add");
            addButton.Click();
            Thread.Sleep(500);
            var windowAddToArchive = desktopDriver.FindElementByName("Add to Archive");
            var archiveNameField = windowAddToArchive.FindElementByAccessibilityId("100");
            archiveNameField.SendKeys(archiveFileName);
            var archiveFormatMenu = desktopDriver.FindElementByAccessibilityId("104");
            archiveFormatMenu.SendKeys("7z");
            var comperisonLevelMenu = windowAddToArchive.FindElementByAccessibilityId("102");
            comperisonLevelMenu.SendKeys("9 - Ultra");
            var comperisonMethodMenu = windowAddToArchive.FindElementByAccessibilityId("106");
            comperisonMethodMenu.SendKeys("LZMA");
            var dictionarySizeMenu = windowAddToArchive.FindElementByAccessibilityId("107");
            dictionarySizeMenu.SendKeys("64 KB");
            var wordSizeMenu = windowAddToArchive.FindElementByAccessibilityId("108");
            wordSizeMenu.SendKeys("32");
            var solidBlockSizeMenu = windowAddToArchive.FindElementByAccessibilityId("109");
            solidBlockSizeMenu.SendKeys("64 MB");
            var addToArchiveOkButton = windowAddToArchive.FindElementByAccessibilityId("1");
            addToArchiveOkButton.Click();
            Thread.Sleep(500);
            locationFolderField.SendKeys(workDir + Keys.Enter);  
            var archiveFile = driver.FindElementByName(archiveName);
            Assert.IsTrue(archiveFile.Displayed);
        }

        [Test,Order(2)]
        public void Test_7ZipApp_ExtractArchive()
        {
            var locationFolderField = driver.FindElementByAccessibilityId("1003");
            locationFolderField.SendKeys(workDir + Keys.Enter);
            var filesList = driver.FindElementByClassName("SysListView32");
            filesList.SendKeys(Keys.Control + 'a');
            var extractButton = driver.FindElementByName("Extract");
            extractButton.Click();
            var windowExtract = desktopDriver.FindElementByClassName("#32770");
            var extractOkButton = windowExtract.FindElementByAccessibilityId("1");
            extractOkButton.Click();    
            Thread.Sleep(500);
            var extractFolder = driver.FindElementByName(archiveName.Split(".")[0]);
            extractFolder.SendKeys(Keys.Enter);
            var exeFile = driver.FindElementByName("7zFM.exe");
            Assert.IsTrue(exeFile.Displayed);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
