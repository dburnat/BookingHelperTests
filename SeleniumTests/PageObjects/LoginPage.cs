using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumTests.PageObjects
{
    public class RegisterPage
    {
        private readonly IWebDriver driver;
        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.Id, Using = "email")]
        public IWebElement UserName { get; set; }
        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement Password { get; set; }
        [FindsBy(How = How.Name, Using = "submit")]
        public IWebElement Submit { get; set; }
    }
}
