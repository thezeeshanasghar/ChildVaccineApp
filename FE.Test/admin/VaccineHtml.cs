using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace FE.Test.admin
{
    [TestClass]
    public class VaccineHTML : TestBase
    {
        [TestMethod, TestCategory("Vaccine")]
        public void Add_Vaccine_success()
        {
           // var Name = "test Case";
            var MinAge = "6";
            var MaxAge = "7";
            driver.Url = baseURL + "admin/vaccine.html";
           driver.FindElement(By.Id("btnModal")).Click();         
            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait1.Timeout = TimeSpan.FromSeconds(15);
            // driver.FindElement(By.Id("Name")).Click();
            //driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("test case");
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            // wait1.Until(x=> x.Equals("test case"));
            driver.FindElement(By.Id("MinAge")).SendKeys(MinAge);          
            driver.FindElement(By.Id("MaxAge")).SendKeys(MaxAge);          
            driver.FindElement(By.Id("btnAdd")).Click();            
             WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement table = driver.FindElement(By.XPath("//*[@id='vaccineTable']/tbody"));
            WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IList<IWebElement> table_Tr = table.FindElements(By.TagName("tr"));
            
            IWebElement row = table_Tr[table_Tr.Count - 1];
            IList<IWebElement> row1 = row.FindElements(By.TagName("td"));
            //Console.WriteLine(row1[1].Text);
            // Console.WriteLine(row1[2].Text);
            //Console.WriteLine(row1[3].Text);
          


            Assert.AreEqual(row1[1].Text,"test case");
            Assert.AreEqual(row1[2].Text,"6 Weeks");
            Assert.AreEqual(row1[3].Text,"7 Weeks");
        


        }

        [TestMethod, TestCategory("Vaccine")]
        public void Update_vaccine_success()
        {
            driver.Url=baseURL + "admin/vaccine.html"; ;
            driver.FindElement(By.Id("btnEdit")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("update Value");
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.FindElement(By.Id("btnUpdate")).Click();
            WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement table = driver.FindElement(By.XPath("//*[@id='vaccineTable']/tbody"));
            WebDriverWait wait4 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IList<IWebElement> table_Tr = table.FindElements(By.TagName("tr"));

            IWebElement row = table_Tr[0];
            IList<IWebElement> row1 = row.FindElements(By.TagName("td"));
            //Console.WriteLine(row1[1].Text);
            // Console.WriteLine(row1[2].Text);
            //Console.WriteLine(row1[3].Text);



            Assert.AreEqual(row1[1].Text,"update Value");
           // Assert.IsTrue(row1[2].Text.Equals("6 Weeks"));
           // Assert.IsTrue(row1[3].Text.Equals("7 Weeks"));


        }

        [TestMethod, TestCategory("Vaccine")]
        public void Delete_Vacine_Success()
        {
            driver.Url = baseURL + "admin/vaccine.html"; ;
            driver.FindElement(By.Id("btnDelete")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var text = (new WebDriverWait(driver, TimeSpan.FromSeconds(10))).Until(d => d.SwitchTo().Alert());

            Assert.AreEqual("Are you sure you want to delete this Record?", text.Text);
            text.Accept();
            //if (text.Text.Equals("Are you sure you want to delete this Record?"))
            //{
            //    text.Accept();

            //}



            //Are you sure you want to delete this Record?

        }
    }
}
