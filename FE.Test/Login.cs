﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System;

namespace FE.Test
{
    [TestClass]
    public class Login : TestBase
    {
        [TestMethod, TestCategory("HappyFlow")]
        public void AdminLogin_CorrectDetails_NavigateToIndex()

        {
           
            driver.Url = baseURL + "login.html";
            driver.FindElement(By.Id("MobileNumber")).SendKeys("3331231231");
            driver.FindElement(By.Id("Password")).SendKeys("1234");
            driver.FindElement(By.Id("btnSignIn")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(x => x.Url == baseURL + "index.html");

            Assert.IsTrue(driver.Url.Equals(baseURL + "index.html"));
            Assert.IsTrue(driver.Title.Equals("Vaccs.io"));
        }

        [TestMethod, TestCategory("UnHappyFlow"), TestCategory("ServerSideValidation")]
        public void AdminLogin_WrongDetails_ShowError()
        {
            driver.Url = baseURL + "login.html";
            driver.FindElement(By.Id("MobileNumber")).SendKeys("3331231231");
            driver.FindElement(By.Id("Password")).SendKeys("12345");
            driver.FindElement(By.Id("btnSignIn")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var alert = wait.Until(d => d.SwitchTo().Alert());

            Assert.AreEqual("Invalid Mobilenumber/Password", alert.Text);
        }

        [TestMethod, TestCategory("UnHappyFlow"), TestCategory("ClientSideValidation")]
        public void AdminLogin_WrongMobileNumber_ShowValidationError()
        {
            driver.Url = baseURL + "login.html";
            driver.FindElement(By.Id("MobileNumber")).SendKeys("333123123");
            driver.FindElement(By.Id("Password")).SendKeys("1234");
            driver.FindElement(By.Id("btnSignIn")).Click();

            Assert.AreEqual("Please match the requested format.", driver.FindElement(By.CssSelector(".help-block")).Text);
        }


    }
}
