using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace home_project
{
    public class HomePage : Page
    {
        internal HomePage(IWebDriver driver) : base(driver) { }

        internal void Open()
        {
            driver.Url = "http://localhost/litecart";
        }
        internal void OpenFirstItem()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='box-most-popular']//li[1]/a"))).Click();
        }
        internal void checkout()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(), 'Checkout')]"))).Click();
        }

    }
}
