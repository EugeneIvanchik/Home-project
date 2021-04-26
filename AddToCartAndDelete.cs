using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AddToCartAndDelete
{
    [TestFixture]
    public class AddToCartAndDelete
    {
        private static readonly string domainPath = AppDomain.CurrentDomain.BaseDirectory + @"resources\ChromeDriver";
        private IWebDriver driver;
        private WebDriverWait wait;
        private IWebElement numberOfItemsInCartElement;
        private int numberOfItemsInCart;
        private int numberOfRecordsInSummary;
        private IWebElement removeButtonElement;
        private string removedItemName;
        private IWebElement tableRecord;
        private IWebElement sizeDropdownElement;
        private SelectElement sizeDropdown;
        private void openFirstItem()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='box-most-popular']//li[1]/a"))).Click();
        }

        private void rememberNumberOfItemsInCart()
        {
            numberOfItemsInCartElement = driver.FindElement(By.XPath("//span[@class='quantity']"));
            numberOfItemsInCart = Convert.ToInt32(numberOfItemsInCartElement.GetAttribute("textContent"));
        }
        private void addToCart()
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
        private void waitCartRefreshed()
        {
            wait.Until(ExpectedConditions.TextToBePresentInElement(numberOfItemsInCartElement, (numberOfItemsInCart + 1).ToString()));
        }
        private void goHome()
        {
            driver.FindElement(By.XPath("//li[@class='general-0']")).Click();
        }

        private void checkout()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(), 'Checkout')]"))).Click();
        }

        private void defineNumberOfRecordsInSummary()
        {
            numberOfRecordsInSummary = driver.FindElements(By.XPath("//div[@id='box-checkout-summary']//td[@class='item']")).Count;
        }

        private void removeItem()
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

        [SetUp]

        public void start()
        {
            driver = new ChromeDriver(domainPath);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [Test]
        public void AddToCartAndDeleteTest()
        {
            driver.Url = "http://localhost/litecart";

            for(int i = 0; i < 3; i++)
            {
                openFirstItem();
                rememberNumberOfItemsInCart();
                addToCart();
                waitCartRefreshed();
                goHome();
            }

            checkout();

            defineNumberOfRecordsInSummary();

            for (int i = 0; i < numberOfRecordsInSummary; i++)
            {
                removeItem();
            }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
