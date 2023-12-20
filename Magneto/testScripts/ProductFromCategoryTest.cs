using Magneto.pageObjects;
using Magneto.utilites;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magneto.testScripts
{
    [TestFixture]
    internal class ProductFromCategoryTest : CoreCodes
    {
        [Test, Category("E2E testing")]
        [TestCase("1")]
        public void ProductBuyingFromCategoryTest(string input)
        {
            CreateLogs();
            var homePage = new MagentoHomePage(driver);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            homePage.SelectNavBarDropdown();
            Log.Information("Selecting from category on navbar");

            homePage.SelectNavbarInsideDropdown();
            Log.Information("Selecting from inside selcted category on navbar");

            var productPage= homePage.SelectProductFromDropDown(input);
            Log.Information("Selecting product-category from inside selcted category on navbar");
            try
            {
                string productSelected = driver.FindElement(By.XPath("(//li[starts-with(@class,'level2')]/a)"+"["+input+"]")).GetAttribute("href");
                string productDisplay = driver.FindElement(By.ClassName("base")).Text.ToLower();
                ScreenShotTest();
                Assert.That(productSelected.Contains(productDisplay));

                LogTestResult("Required field check  ", "Required field check- success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Required field check ", $"Required field check - failed \n Exception: {ex.Message}");

            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            productPage.ClickOnFilterBySize();
            Log.Information("Choosing filter By Size");

            productPage.SelectSize();
            Log.Information("Filtering By Size");

            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            productPage.SelectProductFromList();
            Log.Information("Selecting product after filtering");

            productPage.ClickOnAddToCart();
            Log.Information("Clicked on add to cart");
            try
            {
                string msg=driver.FindElement(By.XPath("(//div[@class='mage-error'])[1]")).Text;
                ScreenShotTest();
                Assert.That(msg.Contains("required"));

                LogTestResult("Required field check  ", "Required field check- success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Required field check ", $"Required field check - failed \n Exception: {ex.Message}");

            }


        }
    }
}
