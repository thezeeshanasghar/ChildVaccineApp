using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace FE.Test.admin
{
    [TestClass]
    public class Brand:TestBase
    {
        [TestMethod, TestCategory("Brand")]
        public void Add_Brand_Success()
        {
            driver.Url = baseURL + "admin/vaccine.html"; ;
            driver.FindElement(By.Id("btnBrand")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.FindElement(By.Id("btnModal")).Click();
            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


            driver.FindElement(By.Id("BrandName")).SendKeys("Test Brand");
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.FindElement(By.Id("btnAdd")).Click();

            WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement table = driver.FindElement(By.XPath("//*[@id='brandTable']/tbody"));
            WebDriverWait wait4 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IList<IWebElement> table_Tr = table.FindElements(By.TagName("tr"));

            IWebElement row = table_Tr[table_Tr.Count - 1];
            IList<IWebElement> row1 = row.FindElements(By.TagName("td"));

            Assert.AreEqual(row1[1].Text, "Test Brand");




        }
    }
}
