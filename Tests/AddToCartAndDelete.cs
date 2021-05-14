using System;
using System.Collections.Generic;
using System.Threading;
using home_project;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AddToCartAndDelete
{
    [TestFixture]
    public class AddToCartAndDelete : TestBase
    {
        [Test]
        public void AddToCartAndDeleteTest()
        {
            //add three items into the cart
            for(int i = 0; i < 3; i++)
            {
                app.addFirstItemToCart();
            }

            app.checkOut();

            app.clearCart();
        }
    }
}
