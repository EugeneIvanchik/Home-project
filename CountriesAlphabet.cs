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

namespace CountriesAlphabet
{
    [TestFixture]
    public class CountriesAlphabet
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
        public void CountriesAlphabetTest()
        {
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);
            wait.Until(ExpectedConditions.TitleContains("Countries"));

            IWebElement CountriesForm = driver.FindElement(By.Name("countries_form"));
            IList<IWebElement> CountriesElemArray = (from list in CountriesForm.FindElements(By.CssSelector("a"))
                                                where list.GetAttribute("textContent") != ""
                                                select list).ToList();

            List<string> CountriesStrArray = new List<string>() { };
            List<string> CountriesStrArraySorted = new List<string>() { };

            foreach (IWebElement a in CountriesElemArray)
            {
                CountriesStrArray.Add(a.GetAttribute("textContent"));
                CountriesStrArraySorted.Add(a.GetAttribute("textContent"));
            }

            CountriesStrArraySorted.Sort();
            Assert.IsTrue(CountriesStrArray.SequenceEqual(CountriesStrArraySorted));
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
