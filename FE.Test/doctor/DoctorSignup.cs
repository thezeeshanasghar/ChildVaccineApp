using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace FE.Test.doctor
{
    [TestClass]
    public class DoctorSignup:TestBase
    {
        WebDriverWait wait;
         public DoctorSignup()
        {
            wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));

        }
        [TestMethod ,TestCategory("DoctorSignup")]
        public void DoctorSignup_CorrectDetails_DoctorSignedup()
        {
            driver.Url = baseURL + "doctor/doctor-signup.html";
            wait.Until(MyCondition(By.Id("FirstName")));
            driver.FindElement(By.Id("FirstName")).SendKeys("Qasim");
            wait.Until(MyCondition(By.Id("LastName")));
            driver.FindElement(By.Id("LastName")).SendKeys("ali");
            wait.Until(MyCondition(By.Id("Email")));
            driver.FindElement(By.Id("Email")).SendKeys("ejazsidhu@hotmail.com");
            //wait.Until(MyCondition(By.Id("CountryCode")));
            //driver.FindElement(By.Id("CountryCode")).SendKeys("92");
            wait.Until(MyCondition(By.Id("MobileNumber")));
            driver.FindElement(By.Id("MobileNumber")).SendKeys("3244221345");
            wait.Until(MyCondition(By.Id("PhoneNo")));
            driver.FindElement(By.Id("PhoneNo")).SendKeys("03244221345");

            wait.Until(MyCondition(By.Id("PMDC")));
            driver.FindElement(By.Id("PMDC")).SendKeys("13462-a");
            driver.FindElement(By.Id("btnAdd")).Click();

            //clinic form start here

            wait.Until(MyCondition(By.Id("Name")));
            driver.FindElement(By.Id("Name")).SendKeys("03244221342");
            wait.Until(MyCondition(By.Id("PhoneNumber")));
            driver.FindElement(By.Id("PhoneNumber")).SendKeys("03244221346");
            wait.Until(MyCondition(By.Id("Sunday")));
            driver.FindElement(By.Id("Sunday")).Click();
            wait.Until(MyCondition(By.Id("Saturday")));
            driver.FindElement(By.Id("Saturday")).Click();
            wait.Until(MyCondition(By.Id("StartTime")));
            driver.FindElement(By.Id("StartTime")).Clear();
            driver.FindElement(By.Id("StartTime")).SendKeys("3:00");
            wait.Until(MyCondition(By.Id("EndTime")));
            driver.FindElement(By.Id("EndTime")).Clear();
            driver.FindElement(By.Id("EndTime")).SendKeys("15:00");

            driver.FindElement(By.Id("btnAddClinic")).Click();

            wait.Until(MyCondition(By.Id("alert_message")));

            var text=driver.FindElement(By.Id("alert_message"));
            Console.WriteLine(text.Text);
            Assert.IsTrue(text.Text.Contains("Your are successfully singup for MyVacc"));

        }
    }
}
