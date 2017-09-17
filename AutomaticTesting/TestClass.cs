using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTesting
{
    [TestFixture]
    public class TestClass
    {
        protected IWebDriver driver;
        protected string baseURL;
        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://www.vac.afz-sol.com/";
            driver.Manage().Window.Maximize();
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

        }
        [Test]
        public void VerfyLogin()
        {

            driver.Url = baseURL + "login.html";
            driver.FindElement(By.Id("MobileNumber")).SendKeys("12121");
            driver.FindElement(By.Id("Password")).SendKeys("abc");
            driver.FindElement(By.XPath("//*[@id=\"form1\"]/div/button")).Click();
            // this.CloseConnection();
        }
        public void CloseConnection()
        {
            driver.Close();
            driver.Dispose();
        }
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            Assert.Pass("Your first passing test");
        }
    }
}
