using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AdminSearch
{
    [TestFixture]
    public class AdminSearch
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        

        [SetUp]

        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [Test]
        public void AdminSearchTest()
        {
            driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);
            wait.Until(ExpectedConditions.TitleIs("My Store"));
            IList< IWebElement > parentApps = driver.FindElements(By.Id("app-"));
            IList< IWebElement > childApps;
            for (int i = 0; i < parentApps.Count; i++)
            {
                Thread.Sleep(500);
                parentApps[i].Click();
                Thread.Sleep(500);
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector("h1")));
                
                if (driver.FindElements(By.ClassName("docs")).Count > 0)
                {
                    childApps = driver.FindElements(By.CssSelector("ul.docs > li"));
                    for (int j = 0; j < childApps.Count; j++)
                    {
                        Thread.Sleep(200);
                        childApps[j].Click();
                        Thread.Sleep(300);
                        wait.Until(ExpectedConditions.ElementExists(By.CssSelector("h1")));
                        childApps = driver.FindElements(By.CssSelector("ul.docs > li"));
                    }
                }
                parentApps = driver.FindElements(By.Id("app-"));

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
