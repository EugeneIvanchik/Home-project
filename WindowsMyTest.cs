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

namespace WindowsMyTest
{
    [TestFixture]
    public class WindowsMyTest
    {
        private static readonly string domainPath = AppDomain.CurrentDomain.BaseDirectory + @"resources\ChromeDriver";
        private IWebDriver driver;
        private WebDriverWait wait;
        private string mainWindow;
        private List<string> oldWindowsCollection = new List<string>();
        private List<string> newWindowsCollection = new List<string>();
        private int oldWindowsCollectionCount;

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

        private string otherThanOldWindows(List<string> oldWindowsCollection)
        {
            for (int i = 0; i < oldWindowsCollection.Count; i++)
            {
                newWindowsCollection.Remove(oldWindowsCollection[i]);
            }
            return newWindowsCollection.Count > 0 ? newWindowsCollection[0] : null;

        }

        [SetUp]

        public void start()
        {
            driver = new ChromeDriver(domainPath);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [Test]
        public void WindowsMyTestTest()
        {
            goToURL("http://localhost/litecart/admin/?app=countries&doc=countries");

            login();

            wait.Until(ExpectedConditions.TitleContains("Countries"));

            openCountry("Andorra");

            wait.Until(ExpectedConditions.TitleContains("Edit Country"));

            mainWindow = driver.CurrentWindowHandle;
            for (int i = 0; i < driver.WindowHandles.Count; i++)
            {
                oldWindowsCollection.Insert(i, driver.WindowHandles[i]);
            }
            //oldWindowsCollection = driver.WindowHandles;
            oldWindowsCollectionCount = oldWindowsCollection.Count;

            ReadOnlyCollection<IWebElement> linksCollection = driver.FindElements(By.XPath("//form//a[@target='_blank']"));

            for (int i = 0; i < linksCollection.Count; i++)
            {
                linksCollection[i].Click();
                for (int j = 0; j < driver.WindowHandles.Count; j++)
                {
                    newWindowsCollection.Insert(j, driver.WindowHandles[j]);
                }
                //newWindowsCollection = driver.WindowHandles;
                string newWindow = wait.Until(driver => otherThanOldWindows(oldWindowsCollection));
                driver.SwitchTo().Window(newWindow);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
                //linksCollection[i].Click();
                //wait.Until(driver => driver.WindowHandles.Count == (oldWindowsCollectionCount + 1));
                //driver.SwitchTo().Window(driver.WindowHandles.Last());
                //driver.Close();
                //driver.SwitchTo().Window(mainWindow);

            }
        }

        [TearDown]
        public void stop()
        {
            //driver.Quit();
            //driver = null;
        }
    }
}
