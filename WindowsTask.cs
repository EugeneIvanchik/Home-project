using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace WindowsTask
{
    [TestFixture]
    public class WindowsTask
    {
        private static readonly string domainPath = AppDomain.CurrentDomain.BaseDirectory + @"resources\ChromeDriver";
        private IWebDriver driver;
        private WebDriverWait wait;
        private string mainWindow;
        private int oldWindowsCollectionCount;
        private ReadOnlyCollection<IWebElement> linksCollection;

        private void login()
        {
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);
        }

        private void goToURL(string url)
        {
            driver.Url = url;
        }

        private void openCountry(string country)
        {
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//a[contains(text(),'" + country + "')]")).Click();
        }

        [SetUp]

        public void start()
        {
            driver = new ChromeDriver(domainPath);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [Test]
        public void WindowsTaskTest()
        {
            goToURL("http://localhost/litecart/admin/?app=countries&doc=countries");

            login();

            wait.Until(ExpectedConditions.TitleContains("Countries"));

            openCountry("Andorra");

            wait.Until(ExpectedConditions.TitleContains("Edit Country"));

            oldWindowsCollectionCount = driver.WindowHandles.Count;
            mainWindow = driver.CurrentWindowHandle;
            linksCollection = driver.FindElements(By.XPath("//form//a[@target='_blank']"));

            for(int i = 0; i < linksCollection.Count; i++)
            {
                linksCollection[i].Click();
                wait.Until(driver => driver.WindowHandles.Count == (oldWindowsCollectionCount + 1));
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
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
