using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace WSEI_aspnet_project_tests.SeleniumTests
{
	[TestFixture]
	public class LoginTest
	{
        IWebDriver _driver;

        [SetUp]
        public void StartBrowser()
        {
            _driver = new ChromeDriver(@"C:\Users\wizze\source\repos\WSEI-aspnet-poject\drivers");
        }
        [Test]
        public void GoogleTest()
        {
            var login = "test1@test.pl";
            _driver.Navigate().GoToUrl("https://localhost:44382/Identity/Account/Login");
            var searchLoginBox = _driver.FindElement(By.Id("Input_Email"));
            searchLoginBox.SendKeys(login);

            var password = "Test123!";
            var searchPasswordBox = _driver.FindElement(By.Id("Input_Password"));
            searchPasswordBox.SendKeys(password + Keys.Enter);

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
          .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists((By.ClassName("navbar-toggler"))));
          
            StringAssert.StartsWith("https://localhost:44382/", _driver.Url);
        }
        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }

    }
}
