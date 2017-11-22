using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace FE.Test.doctor
{
    [TestClass]
    public class DoctorSignup : TestBase
    {
        WebDriverWait wait;
        public DoctorSignup()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }
        [TestMethod, TestCategory("DoctorSignup")]
        public void DoctorSignup_CorrectDetails_DoctorSignedup()
        {
            driver.Url = baseURL + "doctor/doctor-signup.html";
            wait.Until(MyCondition(By.Id("FirstName")));
            driver.FindElement(By.Id("FirstName")).SendKeys("Zeeshan");
            wait.Until(MyCondition(By.Id("LastName")));
            driver.FindElement(By.Id("LastName")).SendKeys("Asghar");
            wait.Until(MyCondition(By.Id("Email")));
            driver.FindElement(By.Id("Email")).SendKeys("m.zeeshan.asghar@live.com");
            wait.Until(MyCondition(By.Id("MobileNumber")));
            driver.FindElement(By.Id("MobileNumber")).SendKeys("3345022330");
            wait.Until(MyCondition(By.Id("PhoneNo")));
            driver.FindElement(By.Id("PhoneNo")).SendKeys("0511231231");

            wait.Until(MyCondition(By.Id("PMDC")));
            driver.FindElement(By.Id("PMDC")).SendKeys("13462-a");
            wait.Until(MyCondition(By.Id("ConsultationFee")));
            driver.FindElement(By.Id("ConsultationFee")).SendKeys("1000");

            wait.Until(MyCondition(By.Id("btnAdd")));
            driver.FindElement(By.Id("btnAdd")).Click();

            //clinic form start here

            wait.Until(MyCondition(By.Id("Name")));
            driver.FindElement(By.Id("Name")).SendKeys("G-14");
            wait.Until(MyCondition(By.Id("PhoneNumber")));
            driver.FindElement(By.Id("PhoneNumber")).SendKeys("0511231231");
            wait.Until(MyCondition(By.Id("Sunday")));
            driver.FindElement(By.Id("Sunday")).Click();
            wait.Until(MyCondition(By.Id("Saturday")));
            driver.FindElement(By.Id("Saturday")).Click();
            wait.Until(MyCondition(By.Id("StartTime")));
            driver.FindElement(By.Id("StartTime")).Clear();
            driver.FindElement(By.Id("StartTime")).SendKeys("15:00");
            wait.Until(MyCondition(By.Id("EndTime")));
            driver.FindElement(By.Id("EndTime")).Clear();
            driver.FindElement(By.Id("EndTime")).SendKeys("17:00");

            driver.FindElement(By.Id("btnAddClinic")).Click();

            wait.Until(MyCondition(By.Id("alert_message")));

            var text = driver.FindElement(By.Id("alert_message"));
            Console.WriteLine(text.Text);
            Assert.IsTrue(text.Text.Contains("Your are successfully singup for MyVacc"));

        }
    }
}
