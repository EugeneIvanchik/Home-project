using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace home_project
{
    public class CartPage : Page
    {
        internal int numberOfRecordsInSummary;
        internal IWebElement removeButtonElement;
        internal string removedItemName;
        internal IWebElement tableRecord;
        internal CartPage(IWebDriver driver) : base(driver) { }

        internal void defineNumberOfRecordsInSummary()
        {
            numberOfRecordsInSummary = driver.FindElements(By.XPath("//div[@id='box-checkout-summary']//td[@class='item']")).Count;
        }
        internal void removeItem()
        {
            try
            {
                driver.FindElement(By.XPath("//ul[@class='shortcuts']//a")).Click();
            }
            catch
            {

            }
            removeButtonElement = driver.FindElement(By.XPath("//button[contains(text(), 'Remove')]"));
            removedItemName = driver.FindElement(By.XPath("//button[contains(text(), 'Remove')]/../../p/a")).Text;
            tableRecord = driver.FindElement(By.XPath("//td[contains(text(), '" + removedItemName + "')]"));
            Thread.Sleep(500);
            removeButtonElement.Click();
            wait.Until(ExpectedConditions.StalenessOf(tableRecord));
        }

        internal void clearcart()
        {
            for (int i = 0; i < numberOfRecordsInSummary; i++)
            {
                removeItem();
            }
        }
    }
}
