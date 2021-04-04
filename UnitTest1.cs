using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1
{
    [TestFixture]
    public class UnitTest1
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]

        public void start()
        {
            driver = new ChromeDriver(@"C:\Users\acer\source\repos\home project\resources");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void FirstTest()
        {
            driver.Url = "https://www.kinopoisk.ru//";
            driver.FindElement(By.Name("kp_query")).SendKeys("John Wick");
            driver.FindElement(By.Name("kp_query")).SendKeys(Keys.Enter);
            wait.Until(ExpectedConditions.TitleIs("Результаты поиска (1336)"));
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
