using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;

namespace FE.Test.admin
{
    [TestClass]
    public class Vaccine : TestBase
    {
        WebDriverWait wait;
        public Vaccine()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }
        
        [TestMethod, TestCategory("HappyFlow"), TestCategory("Vaccine")]
        public void AddVaccine_CorrectDetails_VaccineAdded()
        {
            var MinAge = "6";
            var MaxAge = "7";
            driver.Url = baseURL + "admin/vaccine.html";
            driver.FindElement(By.Id("btnModal")).Click();
            
            wait.Until(MyCondition(By.Id("Name")));
            driver.FindElement(By.Id("Name")).SendKeys("test case");
            driver.FindElement(By.Id("MinAge")).SendKeys(MinAge);
            driver.FindElement(By.Id("MaxAge")).SendKeys(MaxAge);
            wait.Until(MyCondition(By.Id("btnAdd")));
            driver.FindElement(By.Id("btnAdd")).Click();

            wait.Until(MyCondition(By.XPath("//*[@id='vaccineTable']/tbody")));
            IWebElement tbody = driver.FindElement(By.XPath("//*[@id='vaccineTable']/tbody"));
            IList<IWebElement> trs = tbody.FindElements(By.TagName("tr"));
            IWebElement lastRow = trs[trs.Count - 1];
            IList<IWebElement> tds = lastRow.FindElements(By.TagName("td"));

            Assert.AreEqual(tds[1].Text, "test case");
            Assert.AreEqual(tds[2].Text, "6 Weeks");
            Assert.AreEqual(tds[3].Text, "7 Weeks");
        }

        [TestMethod, TestCategory("HappyFlow"), TestCategory("Vaccine")]
        public void UpdateVaccine_CorrectDetails_VaccineUpdated()
        {
            driver.Url = baseURL + "admin/vaccine.html"; ;
            driver.FindElement(By.Id("btnEdit")).Click();

            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("update Value");
            driver.FindElement(By.Id("btnUpdate")).Click();

            IWebElement table = driver.FindElement(By.XPath("//*[@id='vaccineTable']/tbody"));
            IList<IWebElement> table_Tr = table.FindElements(By.TagName("tr"));
            IWebElement row = table_Tr[0];
            IList<IWebElement> row1 = row.FindElements(By.TagName("td"));

            Assert.AreEqual(row1[1].Text, "update Value");
            Assert.IsTrue(row1[2].Text.Equals("6 Weeks"));
            Assert.IsTrue(row1[3].Text.Equals("7 Weeks"));
        }

        [TestMethod, TestCategory("HappyFlow"), TestCategory("Vaccine")]
        public void DeleteVaccine_CorrectDetails_VaccineDeleted()
        {
            driver.Url = baseURL + "admin/vaccine.html"; ;
            driver.FindElement(By.Id("btnDelete")).Click();

            var text = (new WebDriverWait(driver, TimeSpan.FromSeconds(10))).Until(d => d.SwitchTo().Alert());

            Assert.AreEqual("Are you sure you want to delete this Record?", text.Text);
            text.Accept();
          
        }
    }
}
