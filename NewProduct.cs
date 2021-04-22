using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace NewProduct
{
    [TestFixture]
    public class NewProduct
    {
        private static readonly string domainPath = AppDomain.CurrentDomain.BaseDirectory + @"resources\ChromeDriver";
        private static readonly string ballPath = AppDomain.CurrentDomain.BaseDirectory + @"resources\Ball\ball.jfif";
        private string productName = "Ball";
        private IWebDriver driver;
        private WebDriverWait wait;
        private IWebElement dropdownElement;
        private SelectElement dropdown;
        private void byNameSendKeys(string locatorName, string keyValue)
        {
            driver.FindElement(By.Name(locatorName)).SendKeys(keyValue);
        }

        private void chooseFromDropdown(string dropdownGroupName, string textValue)
        {
            dropdownElement = driver.FindElement(By.XPath("//strong[contains(text(), '" + dropdownGroupName + "')]/../select"));
            dropdown = new SelectElement(dropdownElement);
            dropdown.SelectByText(textValue);
        }

        private void chooseFromSpecifiedCheckbox(string checkboxGroupName, string checkboxValue)
        {
            driver.FindElement(By.XPath("//strong[contains(text(), '" + checkboxGroupName + "')]/../div/table/tbody/tr/td[contains(text(), '" + checkboxValue + "')]/../td[1]/input")).Click();
            
        }

        private void clearElementContent(string locatorName)
        {
            driver.FindElement(By.Name(locatorName)).Clear();
        }

        [SetUp]

        public void start()
        {
            driver = new ChromeDriver(domainPath);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [Test]
        public void NewProductTest()
        {
            driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(), 'Catalog')]")));
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//span[contains(text(), 'Catalog')]")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(), ' Add New Product')]")));
            
            driver.FindElement(By.XPath("//a[contains(text(), ' Add New Product')]")).Click();
            Thread.Sleep(500);
            wait.Until(ExpectedConditions.TitleContains("Add New Product"));

            driver.FindElement(By.XPath("//input[@name='status' and @value='1']")).Click();

            byNameSendKeys("name[en]", productName);

            byNameSendKeys("code", "222310");

            chooseFromSpecifiedCheckbox("Categories", "Subcategory");

            chooseFromDropdown("Default Category", "Subcategory");

            chooseFromSpecifiedCheckbox("Product Groups", "Unisex");

            clearElementContent("quantity");

            byNameSendKeys("quantity", "10");

            chooseFromDropdown("Quantity Unit", "pcs");

            chooseFromDropdown("Delivery Status", "3-5 days");

            chooseFromDropdown("Sold Out Status", "Temporary sold out");

            IWebElement fileUploader = driver.FindElement(By.XPath("//input[@type='file' and @name='new_images[]']"));

            fileUploader.SendKeys(ballPath);

            byNameSendKeys("date_valid_from", "04-20-2021");

            byNameSendKeys("date_valid_to", "04-20-2022");

            driver.FindElement(By.XPath("//a[contains(text(), 'Information')]")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//strong[contains(text(), 'Manufacturer')]")));

            chooseFromDropdown("Manufacturer", "ACME Corp.");

            byNameSendKeys("keywords", "KeyPassword");

            byNameSendKeys("short_description[en]", "Best ball you've ever had");

            driver.FindElement(By.XPath("//div[@class='trumbowyg-editor']")).SendKeys("Big Description");

            byNameSendKeys("head_title[en]", "Ball for football");

            byNameSendKeys("meta_description[en]", "MetaBall for Metafootball");

            driver.FindElement(By.XPath("//a[contains(text(), 'Prices')]")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//strong[contains(text(), 'Purchase Price')]")));

            clearElementContent("purchase_price");

            byNameSendKeys("purchase_price", "150");

            chooseFromDropdown("Purchase Price", "Euros");

            clearElementContent("gross_prices[USD]");

            byNameSendKeys("gross_prices[USD]", "165");

            clearElementContent("gross_prices[EUR]");

            byNameSendKeys("gross_prices[EUR]", "132");

            driver.FindElement(By.XPath("//button[@name='save']")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(), '" + productName + "')]")));
            

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
