using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace home_project
{
    public class Application
    {
        private static readonly string domainPath = AppDomain.CurrentDomain.BaseDirectory + @"resources\ChromeDriver";
        private IWebDriver driver;

        private HomePage homePage;
        private ItemDetailsPage itemDetailsPage;
        private CartPage cartPage;

        public Application()
        {
            driver = new ChromeDriver(domainPath);
            homePage = new HomePage(driver);
            itemDetailsPage = new ItemDetailsPage(driver);
            cartPage = new CartPage(driver);
        }

        public void addFirstItemToCart()
        {
            homePage.Open();
            homePage.OpenFirstItem();
            itemDetailsPage.rememberNumberOfItemsInCart();
            itemDetailsPage.addToCart();
            itemDetailsPage.waitCartRefreshed();
            itemDetailsPage.backToHomePage();
        }

        public void checkOut()
        {
            homePage.checkout();
        }

        public void clearCart()
        {
            cartPage.clearcart();
        }

        public void Quit()
        {
            driver.Quit();
        }
    }

}
