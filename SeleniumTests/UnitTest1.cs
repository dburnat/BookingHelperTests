using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.PageObjects;
using System;
using System.IO;

namespace Tests
{
    public class Tests
    {
        private IWebDriver _driver;
        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver(Directory.GetCurrentDirectory());
            _driver.Manage().Window.FullScreen();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void GoogleTest()
        {
            var text = "first";
            _driver.Navigate().GoToUrl("https://www.google.pl/");

            var searchTextBox = _driver.FindElement(By.Name("q"));

            searchTextBox.SendKeys(text + Keys.Enter);
            new WebDriverWait(_driver, TimeSpan.FromSeconds(5))
                .Until(ExpectedConditions.ElementExists(By.CssSelector("#rso > div:nth-child(2) > div > div:nth-child(1) > div > div > div.r > a > h3")));

            StringAssert.StartsWith("https://www.google.pl/search?", _driver.Url);
        }

        [Test]
        public void LoginTest()
        {
            var login = "wsei_test";
            var password = "wsei_test";
            _driver.Url = "https://rori4.github.io/selenium-practice/#/pages/practice/simple-registration";            //var homePage = new HomePage(_driver);
            //homePage.DismissButton.Click();
            //homePage.MyAccount.Click();

            var loginPage = new RegisterPage(_driver);
            loginPage.UserName.SendKeys(login);
            loginPage.Password.SendKeys(password);
            loginPage.Submit.Submit();

            //Assert.IsTrue

            //Assert.IsTrue(homePage.HeaderText.Displayed);
            //Assert.AreEqual("Your Account", homePage.HeaderText.Text);
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}