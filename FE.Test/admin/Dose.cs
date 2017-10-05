using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace FE.Test.admin
{
    [TestClass]
    public class Dose:TestBase
    {
       
        WebDriverWait wait;
        public Dose()
        {
            wait = new WebDriverWait(driver,TimeSpan.FromSeconds(15));
        }

        [TestMethod, TestCategory("Dose")]

        public void AddDosage_CorrecyDetail_DoseAdded()
        {
            driver.Url = baseURL + "admin/vaccine.html";
            wait.Until(MyCondition(By.Id("btnDose")));
            driver.FindElement(By.Id("btnDose")).Click();
            driver.FindElement(By.Id("btnModal")).Click();
            wait.Until(MyCondition(By.Id("Name")));
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("test Dosage");
            wait.Until(MyCondition(By.Id("GapInDays")));
            driver.FindElement(By.Id("GapInDays")).Clear();
            driver.FindElement(By.Id("GapInDays")).SendKeys("test Dosage Gap");
            wait.Until(MyCondition(By.Id("DoseOrder")));
            driver.FindElement(By.Id("DoseOrder")).Clear();
            driver.FindElement(By.Id("DoseOrder")).SendKeys("test Dosage Order");
            driver.FindElement(By.Id("btnAdd")).Click();

            IWebElement table = driver.FindElement(By.XPath("//*[@id='doseTable']/tbody"));
            IList<IWebElement> table_Tr = table.FindElements(By.TagName("tr"));
            IWebElement row = table_Tr[table_Tr.Count - 1];
            IList<IWebElement> row1 = row.FindElements(By.TagName("td"));
            Assert.IsTrue(row1[1].Text.Equals("test Dosage"));
        }

        [TestMethod, TestCategory("Dose")]

        public void UpdateDose_CorrectDetail_DoseUpdated()
        {
            driver.Url = baseURL + "admin/vaccine.html";
            wait.Until(MyCondition(By.Id("btnDose")));
            driver.FindElement(By.Id("btnDose")).Click();

            wait.Until(MyCondition(By.Id("btnEditDose")));
            driver.FindElement(By.Id("btnEditDose")).Click();

            wait.Until(MyCondition(By.Id("Name")));
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("test Dosage Updated");
            wait.Until(MyCondition(By.Id("GapInDays")));
            driver.FindElement(By.Id("GapInDays")).Clear();
            driver.FindElement(By.Id("GapInDays")).SendKeys("test Dosage Gap");
            wait.Until(MyCondition(By.Id("DoseOrder")));
            driver.FindElement(By.Id("DoseOrder")).Clear();
            driver.FindElement(By.Id("DoseOrder")).SendKeys("test Dosage Order");
            driver.FindElement(By.Id("btnUpdate")).Click();

            IWebElement table = driver.FindElement(By.XPath("//*[@id='doseTable']/tbody"));
            IList<IWebElement> table_Tr = table.FindElements(By.TagName("tr"));
            IWebElement row = table_Tr[0];
            IList<IWebElement> row1 = row.FindElements(By.TagName("td"));
            Assert.IsTrue(row1[1].Text.Equals("test Dosage Updated"));


        }
        [TestMethod, TestCategory("Dose")]
        public void DeleteDose_CorrectDetail_DoseAdded()
        {
            driver.Url = baseURL + "admin/vaccine.html";
            wait.Until(MyCondition(By.Id("btnDose")));
            driver.FindElement(By.Id("btnDose")).Click();

            wait.Until(MyCondition(By.Id("btnDeleteDose")));
            driver.FindElement(By.Id("btnDeleteDose")).Click();

            var text = wait.Until(d => d.SwitchTo().Alert());

            Assert.IsTrue(text.Text.Equals("Are you sure you want to delete this Record?"));
            text.Accept();
        }
    }
}
