using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Registration
{
    [TestFixture]
    public class Registration
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private IWebElement dropdownElement;
        private SelectElement dropdown;
        private string email = "E90dot@gmail.com";
        private string password = "123qweQWE!@#";
        private string firstname = "Frank";
        private string lastname = "Noname";
        private string phone = "+2002000000";
        private string address1 = "Pushkina str 10";
        private string postcode = "19405";
        private string city = "San Francisco";
        private void byNameSendKeys(string locatorName, string keyValue)
        {
            driver.FindElement(By.Name(locatorName)).SendKeys(keyValue);
        }

        public void unhide(IWebDriver driver, IWebElement element)
        {
            (driver as IJavaScriptExecutor).ExecuteScript("arguments[0].style.opacity=1;"
              + "arguments[0].style.width=100px;"
              + "arguments[0].style.height=20px;", element);
        }

        private void chooseFromDropdown(string textValue)
        {
            dropdown = new SelectElement(dropdownElement);
            dropdown.SelectByText(textValue);
        }

        [SetUp]

        public void start()
        {
            driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [Test]
        public void RegistrationTest()
        {
            driver.Url = "http://localhost/litecart";

            wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));

            IWebElement registrationLink = driver.FindElement(By.XPath("//*[contains(text(), 'New customers click here')]"));
            Thread.Sleep(500);
            registrationLink.Click();
            
            wait.Until(ExpectedConditions.TitleIs("Create Account | My Store"));
            
            Thread.Sleep(500);
            
            byNameSendKeys("firstname", firstname);
            byNameSendKeys("lastname", lastname);
            byNameSendKeys("address1", address1);
            byNameSendKeys("postcode", postcode);
            byNameSendKeys("city", city);

            //dropdownElement = driver.FindElement(By.CssSelector("select[name='country_code']"));
            //unhide(driver, dropdownElement);
            //chooseFromDropdown("United States");
            //не смог сделать этот Select элемент доступным для firefox при помощи JS, пришлось искать через Span
            //если есть возможность, подскажите в комментариях, как сделать select доступным

            driver.FindElement(By.XPath("//td[contains(text(), 'Country')]/span[@dir='ltr']")).Click();
            driver.FindElement(By.XPath("//input[@class='select2-search__field']")).SendKeys("United States");
            driver.FindElement(By.XPath("//input[@class='select2-search__field']")).SendKeys(Keys.Enter);

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("select[name='zone_code']")));
            dropdownElement = driver.FindElement(By.CssSelector("select[name='zone_code']"));
            chooseFromDropdown("California");

            byNameSendKeys("email", email);
            byNameSendKeys("phone", phone);
            byNameSendKeys("password", password);
            byNameSendKeys("confirmed_password", password);
            driver.FindElement(By.Name("newsletter")).Click();

            driver.FindElement(By.Name("create_account")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='notices']/div[contains(text(), ' Your customer account has been created.')]")));

            IWebElement logoutLink = driver.FindElement(By.XPath("//*[contains(text(), 'Logout')]"));
            logoutLink.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='notices']/div[contains(text(), 'You are now logged out.')]")));

            byNameSendKeys("email", email);
            byNameSendKeys("password", password);
            driver.FindElement(By.XPath("//*[@name='login']")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='notices']/div[contains(text(), 'You are now logged in as')]")));

            logoutLink = driver.FindElement(By.XPath("//*[contains(text(), 'Logout')]"));
            logoutLink.Click();

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
