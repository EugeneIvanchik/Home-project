using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace FindStickers
{
    [TestFixture]
    public class FindStickers
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
        public void FindStickersTest()
        {
            driver.Url = "http://localhost/litecart";
            wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));
            IList<IWebElement> icons = driver.FindElements(By.ClassName("image-wrapper"));
            for(int i = 0; i < icons.Count; i++)
            {
                Assert.AreEqual(icons[i].FindElements(By.CssSelector("div[class *= 'sticker']")).Count, 1);
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
