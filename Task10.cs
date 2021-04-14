using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Task10
{
    [TestFixture]
    public class Task10
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        private bool IsGrey(string str)
        {
            string withoutRGBA = str.Substring(4);
            string withoutBrackets = withoutRGBA.Trim(new Char[] { '(', ')' });
            string[] stringResultArray = withoutBrackets.Split(", ");
            if (stringResultArray[0].Equals(stringResultArray[1]) && stringResultArray[0].Equals(stringResultArray[2]))
            {
                return true;
            }
            else return false;

        }
        private bool IsRed(string str)
        {
            string withoutRGBA = str.Substring(4);
            string withoutBrackets = withoutRGBA.Trim(new Char[] { '(', ')' });
            string[] stringResultArray = withoutBrackets.Split(", ");
            if (stringResultArray[1].Equals("0") && stringResultArray[2].Equals("0"))
            {
                return true;
            }
            else return false;

        }
        private bool CampaingFontSizeBiggerThanRegular(string str1, string str2)
        {
            int source1 = Convert.ToInt32(str1.Substring(0, 2));
            int source2 = Convert.ToInt32(str2.Substring(0, 2));

            if (source1>source2)
            {
                return true;
            }
            else return false;

        }

        [SetUp]

        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [Test]
        public void Task10Test()
        {
            driver.Url = "http://localhost/litecart/";
            //driver.FindElement(By.Name("username")).SendKeys("admin");
            //driver.FindElement(By.Name("password")).SendKeys("admin");
            //driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);
            wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));

            string mainPageName = driver.FindElement(By.XPath("//*[@id='box-campaigns']/div/ul/li/a[1]/div[@class='name']")).GetAttribute("textContent");
            string mainPageCampaingPriceText = driver.FindElement(By.XPath("//*[@id='box-campaigns']/div/ul/li/a[1]/div[@class='price-wrapper']/strong")).GetAttribute("textContent");
            string mainPageCampaingPriceTagName = driver.FindElement(By.XPath("//*[@id='box-campaigns']/div/ul/li/a[1]/div[@class='price-wrapper']/strong")).GetAttribute("tagName");
            string mainPageCampaingPriceFontSize = driver.FindElement(By.XPath("//*[@id='box-campaigns']/div/ul/li/a[1]/div[@class='price-wrapper']/strong")).GetCssValue("font-size");
            string mainPageCampaingPriceColor = driver.FindElement(By.XPath("//*[@id='box-campaigns']/div/ul/li/a[1]/div[@class='price-wrapper']/strong")).GetCssValue("color");
            string mainPageRegularPriceColor = driver.FindElement(By.XPath("//*[@id='box-campaigns']/div/ul/li/a[1]/div[@class='price-wrapper']/s")).GetCssValue("color");
            string mainPageRegularPriceTextDecoration = driver.FindElement(By.XPath("//*[@id='box-campaigns']/div/ul/li/a[1]/div[@class='price-wrapper']/s")).GetCssValue("text-decoration");
            string mainPageRegularPriceFontSize = driver.FindElement(By.XPath("//*[@id='box-campaigns']/div/ul/li/a[1]/div[@class='price-wrapper']/s")).GetCssValue("font-size");

            driver.FindElement(By.XPath("//*[@id='box-campaigns']/div/ul/li/a[1]")).Click();
            Thread.Sleep(500);
            wait.Until(ExpectedConditions.TitleIs("Yellow Duck | Subcategory | Rubber Ducks | My Store"));

            string detailsPageName = driver.FindElement(By.XPath("//h1")).GetAttribute("textContent");
            string detailsPageCampaingPriceText = driver.FindElement(By.XPath("//*[@class='campaign-price']")).GetAttribute("textContent");
            string detailsPageCampaingPriceTagName = driver.FindElement(By.XPath("//*[@class='campaign-price']")).GetAttribute("tagName");
            string detailsPageCampaingPriceFontSize = driver.FindElement(By.XPath("//*[@class='campaign-price']")).GetCssValue("font-size");
            string detailsPageCampaingPriceColor = driver.FindElement(By.XPath("//*[@class='campaign-price']")).GetCssValue("color");
            string detailsPageRegularPriceColor = driver.FindElement(By.XPath("//*[@class='information']/div[@class='price-wrapper']/s")).GetCssValue("color");
            string detailsPageRegularPriceTextDecoration = driver.FindElement(By.XPath("//*[@class='information']/div[@class='price-wrapper']/s")).GetCssValue("text-decoration");
            string detailsPageRegularPriceFontSize = driver.FindElement(By.XPath("//*[@class='information']/div[@class='price-wrapper']/s")).GetCssValue("font-size");

            Assert.IsTrue(mainPageName.Equals(detailsPageName), "Names are not equal");

            Assert.IsTrue(mainPageCampaingPriceText.Equals(detailsPageCampaingPriceText), "Prices are not equal");

            Assert.IsTrue(IsGrey(mainPageRegularPriceColor), "Main page: Regular price color is not grey");
            Assert.IsTrue(IsGrey(detailsPageRegularPriceColor), "Details page: Regular price color is not grey");

            Assert.IsTrue(mainPageRegularPriceTextDecoration.Contains("line-through"), "Main page: Regular price is not crossed");
            Assert.IsTrue(detailsPageRegularPriceTextDecoration.Contains("line-through"), "Details page: Regular price is not crossed");

            Assert.IsTrue(mainPageCampaingPriceTagName.Equals("STRONG"), "Main page: Campaing price is not bold");
            Assert.IsTrue(detailsPageCampaingPriceTagName.Equals("STRONG"), "Details page: Campaing price is not bold");

            Assert.IsTrue(IsRed(mainPageCampaingPriceColor), "Main page: Campaing price color is not red");
            Assert.IsTrue(IsRed(detailsPageCampaingPriceColor), "Details page: Campaing price color is not red");

            Assert.IsTrue(CampaingFontSizeBiggerThanRegular(mainPageCampaingPriceFontSize, mainPageRegularPriceFontSize), "Main page: Campaing price font size is not bigger than regular");
            Assert.IsTrue(CampaingFontSizeBiggerThanRegular(detailsPageCampaingPriceFontSize, detailsPageRegularPriceFontSize), "Details page: Campaing price font size is not bigger than regular");

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
