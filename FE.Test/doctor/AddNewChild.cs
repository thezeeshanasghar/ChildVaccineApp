using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace FE.Test.doctor
{
    [TestClass]
    public class AddNewChild:TestBase
    {
        WebDriverWait wait;
        public AddNewChild()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        [TestMethod , TestCategory("Add New Child")]
        public void AddChild_CorrectDetails_ChildAdded()
        {
            // driver.Url = baseURL + "doctor/add-new-child.html";
            driver.Url = baseURL + "login.html";
            wait.Until(MyCondition(By.Id("MobileNumber")));
            driver.FindElement(By.Id("MobileNumber")).SendKeys("3334126594");

            wait.Until(MyCondition(By.Id("Password")));
            driver.FindElement(By.Id("Password")).SendKeys("8410");
            //btnSignIn

           // wait.Until(MyCondition(By.Id("Password")));
            driver.FindElement(By.Id("btnSignIn")).Click();
            wait.Until(x => x.Url == baseURL + "doctor/clinic-selection.html");
            wait.Until(MyCondition(By.ClassName("badge")));
            driver.FindElement(By.ClassName("badge")).Click();

            //addNewChild

            wait.Until(MyCondition(By.Id("addNewChild")));
            driver.FindElement(By.Id("addNewChild")).Click();
            wait.Until(MyCondition(By.Id("Name")));
            driver.FindElement(By.Id("Name")).SendKeys("Test child");

            wait.Until(MyCondition(By.Id("FatherName")));
            driver.FindElement(By.Id("FatherName")).SendKeys("Test child father");


            wait.Until(MyCondition(By.Id("Email")));
            driver.FindElement(By.Id("Email")).SendKeys("ejazsidhu@gmail.com");

            wait.Until(MyCondition(By.Id("DOB")));
            driver.FindElement(By.Id("DOB")).SendKeys("10-01-2017");

            wait.Until(MyCondition(By.Id("MobileNumber")));
            driver.FindElement(By.Id("MobileNumber")).SendKeys("3334126594");

            wait.Until(MyCondition(By.Id("Monday")));
            driver.FindElement(By.Id("Monday")).Click();
            wait.Until(MyCondition(By.Id("Sunday")));
            driver.FindElement(By.Id("Sunday")).Click();
            wait.Until(MyCondition(By.Id("Saturday")));
            driver.FindElement(By.Id("Saturday")).Click();

            wait.Until(MyCondition(By.Name("gender")));
            driver.FindElement(By.Name("gender")).SendKeys("Boy");

            wait.Until(MyCondition(By.Id("City")));
            driver.FindElement(By.Id("City")).SendKeys("Lahore");

            //this commented out are extra option which are disabled

            //wait.Until(MyCondition(By.Id("TogglePreferredDayOfReminder")));
            //driver.FindElement(By.Id("TogglePreferredDayOfReminder")).Click();

            //wait.Until(MyCondition(By.Id("PreferredDayOfReminder")));
            //driver.FindElement(By.Id("PreferredDayOfReminder")).SendKeys("Three Day Before");

            //wait.Until(MyCondition(By.Id("IsEPIDone")));
            //driver.FindElement(By.Id("IsEPIDone")).Click();
            //wait.Until(MyCondition(By.Id("IsVerified")));
            //driver.FindElement(By.Id("IsVerified")).Click();


            wait.Until(MyCondition(By.Id("btnAdd")));
            driver.FindElement(By.Id("btnAdd")).Click();


            wait.Until(x => x.Url == baseURL + "doctor/child.html?id=4");
            var currentUrl = driver.Url;

            Assert.IsTrue(currentUrl.Contains("doctor/child.html?id=4"));




















        }
    }
}
