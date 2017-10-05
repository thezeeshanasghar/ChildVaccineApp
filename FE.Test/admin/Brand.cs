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
        WebDriverWait wait;
        public Brand()
        {
             wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }
        [TestMethod, TestCategory("Brand")]
        public void Add_Brand_CorrectDetails_BrandAdded()
        {
            driver.Url = baseURL + "admin/vaccine.html"; ;
            wait.Until(MyCondition(By.Id("btnBrand")));
            driver.FindElement(By.Id("btnBrand")).Click();           
            driver.FindElement(By.Id("btnModal")).Click();
            wait.Until(MyCondition(By.Id("BrandName")));
            driver.FindElement(By.Id("BrandName")).SendKeys("Test Brand");
            driver.FindElement(By.Id("btnAdd")).Click();          
            IWebElement table = driver.FindElement(By.XPath("//*[@id='brandTable']/tbody"));            
            IList<IWebElement> table_Tr = table.FindElements(By.TagName("tr"));
            IWebElement row = table_Tr[table_Tr.Count - 1];           
            IList<IWebElement> row1 = row.FindElements(By.TagName("td"));
            Assert.IsTrue(row1[1].Text.Equals("Test Brand"));
        }
        [TestMethod, TestCategory("Brand")]

        public void Update_Barandd_CorrectDetail_BrandUpdated()
        {
            driver.Url = baseURL + "admin/vaccine.html";
            wait.Until(MyCondition(By.Id("btnBrand")));
            driver.FindElement(By.Id("btnBrand")).Click();
            wait.Until(MyCondition(By.Id("btnEditBrand")));
            driver.FindElement(By.Id("btnEditBrand")).Click();

            wait.Until(MyCondition(By.Id("BrandName")));
            driver.FindElement(By.Id("BrandName")).Clear();
            driver.FindElement(By.Id("BrandName")).SendKeys("test brand Update");
            driver.FindElement(By.Id("btnUpdate")).Click();
            IWebElement table = driver.FindElement(By.XPath("//*[@id='brandTable']/tbody"));
            IList<IWebElement> table_Tr = table.FindElements(By.TagName("tr"));
            IWebElement row = table_Tr[0];
            IList<IWebElement> row1 = row.FindElements(By.TagName("td"));
            Assert.IsTrue(row1[1].Text.Equals("test brand Update"));


        }

        [TestMethod, TestCategory("Brand")]

        public void Delete_Brand_CorrectDetail_BrandDeleted()
        {
            driver.Url = baseURL + "admin/vaccine.html";
            wait.Until(MyCondition(By.Id("btnBrand")));
            driver.FindElement(By.Id("btnBrand")).Click();
            wait.Until(MyCondition(By.Id("btnDeleteBrand")));
            driver.FindElement(By.Id("btnDeleteBrand")).Click();
           //Are you sure you want to delete this Record ?
            var text = (new WebDriverWait(driver, TimeSpan.FromSeconds(10))).Until(d => d.SwitchTo().Alert());

            Assert.AreEqual("Are you sure you want to delete this Record?", text.Text);
            text.Accept();

        }


    }
}
