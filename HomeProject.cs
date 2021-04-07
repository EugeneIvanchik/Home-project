using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace HomeProject
{
    [TestFixture]
    public class HomeProject
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
        public void HomeProjectTest()
        {
            driver.Url = "https://www.kinopoisk.ru//";
            driver.FindElement(By.Name("kp_query")).SendKeys("John Wick");
            driver.FindElement(By.Name("kp_query")).SendKeys(Keys.Enter);
            wait.Until(ExpectedConditions.TitleIs("Результаты поиска (1331)"));
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
