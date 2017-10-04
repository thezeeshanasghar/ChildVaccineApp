using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace FE.Test
{
    [TestClass]
    public class TestBase
    {
        protected IWebDriver driver;
        //protected string baseURL = "http://www.vac.afz-sol.com/";
        protected string baseURL = "http://localhost:8080/";

        [TestInitialize]
        public void SetupTest()
        {
            driver = new ChromeDriver();

            //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
            //service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            //driver = new FirefoxDriver();

            driver.Manage().Window.Maximize();
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [TestCleanup]
        public void CloseConnection()
        {
            driver.Close();
            driver.Dispose();
            driver.Quit();
        }

        public static Func<IWebDriver, IWebElement> MyCondition(By locator)
        {
            return (driver) => {
                try
                {
                    var ele = driver.FindElement(locator);
                    return ele.Displayed ? ele : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }
    }
}
