using MagnetoBDD.Hooks;
using MagnetoBDD.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;
using System;
using TechTalk.SpecFlow;

namespace MagnetoBDD.StepDefinitions
{
    [Binding]
    public class SearchAndAddToCartStepDefinitions : CoreCodes
    {
        IWebDriver? driver = AllHooks.driver;
        string? product;

        [Given(@"User will be on home page")]
        public void GivenUserWillBeOnHomePage()
        {
            driver.Url = "https://magento.softwaretestingboard.com/";


        }

        [When(@"User search for a product '([^']*)' and click enter")]
        public void WhenUserSearchForAProductAndClickEnter(string input)
        {
            IWebElement search = driver.FindElement(By.Id("search"));
            search.SendKeys(input);
            search.SendKeys(Keys.Enter);
            Log.Information("Searching for product", input);
        }

        [Then(@"User will be on search result page contains '([^']*)'")]
            public void ThenUserWillBeOnSearchResultPageContains(string input)
            {
                try
                {
                    TakeScreenshots(driver);
                    Assert.That(driver.Url.Contains(input));
                    LogTestResult("Searching product ", "Searching product - success");
                }
                catch (AssertionException ex)
                {
                    LogTestResult("Searching product ", $"Searching product - failed \n Exception: {ex.Message}");

                }

        }

        [When(@"User selects a product at position '([^']*)'")]
        public void WhenUserSelectsAProductAtPosition(string position)
        {
             product = driver.FindElement(By.XPath("(//a[@class='product-item-link'])" + "[" + position + "]")).Text;
            IWebElement selectproduct = driver.FindElement(By.XPath("(//img[@class='product-image-photo'])" + "[" + position + "]"));
            selectproduct.Click();
            Log.Information("Selecting product");
        }

        [Then(@"User will be on the selected product page")]
        public void ThenUserWillBeOnTheSelectedProductPage()
        {
            //Console.WriteLine(product);
            try
            {
                TakeScreenshots(driver);
                Assert.That(product.Equals(driver.FindElement(By.ClassName("base")).Text));

                LogTestResult("Selected product ", "Selected product - success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Selected product ", $"Selected product - failed \n Exception: {ex.Message}");

            }
        }

        [When(@"User selects size and colour")]
        public void WhenUserSelectsSizeAndColour()
        {
            IWebElement size = driver.FindElement(By.XPath("//div[contains(@class,'text') and @index ='1']"));
            size.Click();
            Log.Information("Choosing size");
            IWebElement color = driver.FindElement(By.XPath("//div[contains(@class,'color') and @index ='0']"));
            color.Click();
            Log.Information("Choosing color");

        }

        [When(@"User clicks on add to cart button")]
        public void WhenUserClicksOnAddToCartButton()
        {
            IWebElement addtocart = driver.FindElement(By.Id("product-addtocart-button"));
            addtocart.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Log.Information("Clicked on add to cart button");
        }

        [When(@"User checks cart and click on proceed button")]
        public void WhenUserChecksCartAndClickOnProceedButton()
        {
            Thread.Sleep(3000);
            IWebElement cart = driver.FindElement(By.XPath("//a[contains(@class,'showcart')]"));
            cart.Click();
            Log.Information("Clicked on  cart button");

            DefaultWait<IWebDriver> fwait = new DefaultWait<IWebDriver>(driver);
            fwait.Timeout = TimeSpan.FromSeconds(10);
            fwait.PollingInterval = TimeSpan.FromMilliseconds(150);
            fwait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fwait.Message = "No Element Found";

            IWebElement proceed = fwait.Until(d => d.FindElement(By.Id("top-cart-btn-checkout")));
            Thread.Sleep(4000);
            proceed.Click();
            Log.Information("Clicked on Procced to checkout button");

        }

        [Then(@"User will be on checkoutpage")]
        public void ThenUserWillBeOnCheckoutpage()
        {
            try
            {

                TakeScreenshots(driver);
                Assert.That(driver.Url.Contains("checkout"));

                LogTestResult("Checkout product ", "Checkout product - success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Checkout product ", $"Checkout product - failed \n Exception: {ex.Message}");

            }
        }
    }
}
