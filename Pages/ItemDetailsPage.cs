using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace home_project
{
    public class ItemDetailsPage : Page
    {
        internal IWebElement numberOfItemsInCartElement;
        internal int numberOfItemsInCart;
        internal IWebElement sizeDropdownElement;
        internal SelectElement sizeDropdown;

        public ItemDetailsPage(IWebDriver driver) : base(driver) { }

        internal void rememberNumberOfItemsInCart()
        {
            numberOfItemsInCartElement = driver.FindElement(By.XPath("//span[@class='quantity']"));
            numberOfItemsInCart = Convert.ToInt32(numberOfItemsInCartElement.GetAttribute("textContent"));
        }
        internal void addToCart()
        {
            try
            {
                sizeDropdownElement = driver.FindElement(By.XPath("//select[@name='options[Size]']"));
                sizeDropdown = new SelectElement(sizeDropdownElement);
                sizeDropdown.SelectByIndex(1);
            }

            catch
            {

            }
            Thread.Sleep(500);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(), 'Add To Cart')]"))).Click();
        }
        internal void waitCartRefreshed()
        {
            wait.Until(ExpectedConditions.TextToBePresentInElement(numberOfItemsInCartElement, (numberOfItemsInCart + 1).ToString()));
        }

        internal void backToHomePage()
        {
            driver.FindElement(By.XPath("//li[@class='general-0']")).Click();
        }
    }
}
