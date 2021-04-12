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

namespace GeoZones
{
    [TestFixture]
    public class GeoZones
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
        public void GeoZonesTest()
        {
            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);
            wait.Until(ExpectedConditions.TitleContains("Geo Zones"));
            IWebElement GeoZonesForm = driver.FindElement(By.Name("geo_zones_form"));
            IList<IWebElement> CountriesWithZonesElemArray = (from list in GeoZonesForm.FindElements(By.CssSelector("a"))
                                                              where list.GetAttribute("textContent") != ""
                                                              select list).ToList();

            IList<IWebElement> ZonesElemArray;
            List<string> ZonesStrArray = new List<string> { };
            List<string> ZonesStrArraySorted = new List<string> { };
            
            for (int i = 0; i < CountriesWithZonesElemArray.Count; i ++)
            {
                Thread.Sleep(500);
                CountriesWithZonesElemArray[i].Click();
                wait.Until(ExpectedConditions.TitleContains("Edit Geo Zone"));
                ZonesElemArray = driver.FindElements(By.CssSelector("select[name *= '[zone_code]'] > option[selected = 'selected']"));
                for (int j = 0; j < ZonesElemArray.Count - 1; j++)
                {
                    ZonesStrArray.Add(ZonesElemArray[j].GetAttribute("textContent"));
                    ZonesStrArraySorted.Add(ZonesElemArray[j].GetAttribute("textContent"));
                    ZonesStrArraySorted.Sort();
                }
                Assert.IsTrue(ZonesStrArray.SequenceEqual(ZonesStrArraySorted), "Zones are not alphabetised");
                ZonesStrArray.Clear();
                ZonesStrArraySorted.Clear();
                driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";
                GeoZonesForm = driver.FindElement(By.Name("geo_zones_form"));
                CountriesWithZonesElemArray = (from list in GeoZonesForm.FindElements(By.CssSelector("a"))
                                               where list.GetAttribute("textContent") != ""
                                               select list).ToList();
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
