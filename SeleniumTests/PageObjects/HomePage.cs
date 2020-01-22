using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumTests.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "/html/body/p/a")]
        public IWebElement DismissButton { get; set; }
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/header/div[1]/div/ul[2]/li[2]/a")]
        public IWebElement MyAccount { get; set; }
        [FindsBy(How = How.CssSelector, Using = ".entry-title")]
        public IWebElement HeaderText { get; set; }
        public void GoToPage()
        {
            driver.Navigate().GoToUrl("https://google.pl");
        }
    }
}
