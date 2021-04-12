using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace CountriesToZonesAlphabetFOREACH
{
    [TestFixture]
    public class CountriesToZonesAlphabetFOREACH
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
        public void CountriesToZonesAlphabetFOREACHTest()
        {
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);
            wait.Until(ExpectedConditions.TitleContains("Countries"));

            IList<IWebElement> CountriesWithZonesElemArray = driver.FindElements(By.XPath("//tr/td[6][not(contains(text(), '0'))]/../td[5]/a"));
            
            IList<IWebElement> ZonesElemArray;
            List<string> ZonesStrArray = new List<string> { };
            List<string> ZonesStrArraySorted = new List<string> { };
            
            foreach (IWebElement country in CountriesWithZonesElemArray)
            {
                Thread.Sleep(300);
                country.Click();
                wait.Until(ExpectedConditions.TitleContains("Edit"));
                ZonesElemArray = driver.FindElements(By.XPath("//table[2]/tbody/tr/td[3]"));
                for (int j = 0; j < ZonesElemArray.Count - 1; j++)
                {
                    ZonesStrArray.Add(ZonesElemArray[j].GetAttribute("textContent"));
                    ZonesStrArraySorted.Add(ZonesElemArray[j].GetAttribute("textContent"));
                    ZonesStrArraySorted.Sort();
                    Assert.IsTrue(ZonesStrArray.SequenceEqual(ZonesStrArraySorted), "Zones are not alphabetised");
                }
                ZonesStrArray.Clear();
                ZonesStrArraySorted.Clear();
                driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
                CountriesWithZonesElemArray = driver.FindElements(By.XPath("//tr/td[6][not(contains(text(), '0'))]/../td[5]/a"));
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
