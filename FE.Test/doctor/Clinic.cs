using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace FE.Test.doctor
{
    [TestClass]
    public class Clinic:TestBase
    {
        WebDriverWait wait;
        public Clinic()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        [TestMethod , TestCategory("Clinic")]
        public void AddClinic_CorrectDetails_ClinicAdded()
        {
            driver.Url = baseURL + "login.html";
            wait.Until(MyCondition(By.Id("MobileNumber")));
            driver.FindElement(By.Id("MobileNumber")).SendKeys("3334126594");
            
            wait.Until(MyCondition(By.Id("Password")));
            driver.FindElement(By.Id("Password")).SendKeys("8410");
                        
            driver.FindElement(By.Id("btnSignIn")).Click();
            wait.Until(x => x.Url == baseURL + "doctor/clinic-selection.html");
            wait.Until(MyCondition(By.ClassName("badge")));
            driver.FindElement(By.ClassName("badge")).Click();
            
            wait.Until(MyCondition(By.Id("openSideNav")));
            driver.FindElement(By.Id("openSideNav")).Click();

            wait.Until(MyCondition(By.Id("navBtnClinic")));
            driver.FindElement(By.Id("navBtnClinic")).Click();

            wait.Until(x => x.Url == baseURL + "doctor/clinic.html");


            wait.Until(MyCondition(By.Id("btnModalClinic")));
            driver.FindElement(By.Id("btnModalClinic")).Click();

            wait.Until(MyCondition(By.Id("Name")));
            driver.FindElement(By.Id("Name")).SendKeys("Test Clinic");

            wait.Until(MyCondition(By.Id("PhoneNumber")));
            driver.FindElement(By.Id("PhoneNumber")).SendKeys("03224352689");

            wait.Until(MyCondition(By.Id("Sunday")));
            driver.FindElement(By.Id("Sunday")).Click();
            wait.Until(MyCondition(By.Id("Saturday")));
            driver.FindElement(By.Id("Saturday")).Click();
            wait.Until(MyCondition(By.Id("StartTime")));
            driver.FindElement(By.Id("StartTime")).Clear();
            driver.FindElement(By.Id("StartTime")).SendKeys("09:00");
            
            wait.Until(MyCondition(By.Id("EndTime")));
            driver.FindElement(By.Id("EndTime")).Clear();
            driver.FindElement(By.Id("EndTime")).SendKeys("17:00");

            driver.FindElement(By.Id("btnAdd")).Click();
            wait.Until(x => x.Url == baseURL + "doctor/clinic.html");

            IWebElement table = driver.FindElement(By.XPath("//*[@id='clinicTable']/tbody"));
            IList<IWebElement> table_Tr = table.FindElements(By.TagName("tr"));
            IWebElement row = table_Tr[table_Tr.Count - 1];
            IList<IWebElement> row1 = row.FindElements(By.TagName("td"));
            Assert.IsTrue(row1[1].Text.Equals("Test Clinic"));

        }

        [TestMethod, TestCategory("Clinic")]

        public void UpdateClinic_CorrectDetails_ClinicUpdated()
        {
            driver.Url = baseURL + "login.html";
            wait.Until(MyCondition(By.Id("MobileNumber")));
            driver.FindElement(By.Id("MobileNumber")).SendKeys("3334126594");

            wait.Until(MyCondition(By.Id("Password")));
            driver.FindElement(By.Id("Password")).SendKeys("8410");

            driver.FindElement(By.Id("btnSignIn")).Click();
            wait.Until(x => x.Url == baseURL + "doctor/clinic-selection.html");
            wait.Until(MyCondition(By.ClassName("badge")));
            driver.FindElement(By.ClassName("badge")).Click();

            wait.Until(MyCondition(By.Id("openSideNav")));
            driver.FindElement(By.Id("openSideNav")).Click();

            wait.Until(MyCondition(By.Id("navBtnClinic")));
            driver.FindElement(By.Id("navBtnClinic")).Click();

            wait.Until(x => x.Url == baseURL + "doctor/clinic.html");

            wait.Until(MyCondition(By.Id("btnEditClinic")));
            driver.FindElement(By.Id("btnEditClinic")).Click();

            wait.Until(MyCondition(By.Id("Name")));
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("Test Clinic Update");

            wait.Until(MyCondition(By.Id("PhoneNumber")));
            driver.FindElement(By.Id("PhoneNumber")).Clear();
            driver.FindElement(By.Id("PhoneNumber")).SendKeys("03224352689");

            wait.Until(MyCondition(By.Id("Sunday")));
            driver.FindElement(By.Id("Sunday")).Click();
            wait.Until(MyCondition(By.Id("Saturday")));
            driver.FindElement(By.Id("Saturday")).Click();
            wait.Until(MyCondition(By.Id("StartTime")));
            driver.FindElement(By.Id("StartTime")).Clear();
            driver.FindElement(By.Id("StartTime")).SendKeys("09:00");

            wait.Until(MyCondition(By.Id("EndTime")));
            driver.FindElement(By.Id("EndTime")).Clear();
            driver.FindElement(By.Id("EndTime")).SendKeys("17:00");

            driver.FindElement(By.Id("btnUpdate")).Click();
       
            wait.Until(x => x.Url == baseURL + "doctor/clinic.html");

            IWebElement table = driver.FindElement(By.XPath("//*[@id='clinicTable']/tbody"));
            IList<IWebElement> table_Tr = table.FindElements(By.TagName("tr"));
            IWebElement row = table_Tr[0];
            IList<IWebElement> row1 = row.FindElements(By.TagName("td"));
            Assert.IsTrue(row1[1].Text.Equals("Test Clinic Update"));




        }


        [TestMethod, TestCategory("Clinic")]

        public void DeleteClinic_CorrectDeatils_ClinicDeleted()
        {
            driver.Url = baseURL + "login.html";
            wait.Until(MyCondition(By.Id("MobileNumber")));
            driver.FindElement(By.Id("MobileNumber")).SendKeys("3334126594");

            wait.Until(MyCondition(By.Id("Password")));
            driver.FindElement(By.Id("Password")).SendKeys("8410");

            driver.FindElement(By.Id("btnSignIn")).Click();
            wait.Until(x => x.Url == baseURL + "doctor/clinic-selection.html");
            wait.Until(MyCondition(By.ClassName("badge")));
            driver.FindElement(By.ClassName("badge")).Click();

            wait.Until(MyCondition(By.Id("openSideNav")));
            driver.FindElement(By.Id("openSideNav")).Click();

            wait.Until(MyCondition(By.Id("navBtnClinic")));
            driver.FindElement(By.Id("navBtnClinic")).Click();

            wait.Until(x => x.Url == baseURL + "doctor/clinic.html");

            wait.Until(MyCondition(By.Id("btnDeleteClinic")));
            driver.FindElement(By.Id("btnDeleteClinic")).Click();

            var text = wait.Until(d => d.SwitchTo().Alert());

            Assert.IsTrue(text.Text.Equals("Are you sure you want to delete this Record?"));
            text.Accept();


        }
    }
}
