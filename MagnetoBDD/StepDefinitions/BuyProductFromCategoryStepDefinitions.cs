using MagnetoBDD.Hooks;
using MagnetoBDD.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Serilog;
using System;
using TechTalk.SpecFlow;

namespace MagnetoBDD.StepDefinitions
{
    [Binding]
    public class BuyProductFromCategoryStepDefinitions :CoreCodes
    {
        IWebDriver? driver = AllHooks.driver;
      

        [When(@"User selects category button on navbar")]
        public void WhenUserSelectsCategoryButtonOnNavbar()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            IWebElement selectbycategory = driver.FindElement(By.XPath("(//a[@class='level-top ui-corner-all'])[2]"));
            Actions actions = new Actions(driver);
            Action mouseHover = () => actions
               .MoveToElement(selectbycategory)
               .Build()
               .Perform();
            mouseHover.Invoke();
            Log.Information("Selecting from category on navbar");
        }

        [When(@"selects product category")]
        public void WhenSelectsProductCategory()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            IWebElement selectproductcategory = driver.FindElement(By.XPath("(//a[@class='ui-corner-all'])[1]"));
            Actions actions = new Actions(driver);
            Action mouseHover = () => actions
               .MoveToElement(selectproductcategory)
               .Build()
               .Perform();
            mouseHover.Invoke();
            Log.Information("Selecting from inside selcted category on navbar");
        }

        [When(@"selects product from list")]
        public void WhenSelectsProductFromList()
        {
            IWebElement? selectProduct = driver.FindElement(By.XPath(("(//li[starts-with(@class,'level2')]/a)[1]")));
            selectProduct?.Click();
            Log.Information("Selecting product-category from inside selcted category on navbar");
        }

        [Then(@"User will be on selected product page")]
        public void ThenUserWillBeOnSelectedProductPage()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            try
            {
                string productSelected = driver.FindElement(By.XPath("(//li[starts-with(@class,'level2')]/a)[1]")).GetAttribute("href");
                string productDisplay = driver.FindElement(By.ClassName("base")).Text.ToLower();
                TakeScreenshots(driver);
                Assert.That(productSelected.Contains(productDisplay));

                LogTestResult("Required field check  ", "Required field check- success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Required field check ", $"Required field check - failed \n Exception: {ex.Message}");

            }
        }

        [When(@"Filters product by size")]
        public void WhenFiltersProductBySize()
        {
            IWebElement? selectsize = driver.FindElement(By.XPath(("(//div[@class='filter-options-title'])[2]")));
            selectsize.Click();
            Log.Information("Filtering By Size");
            IWebElement? size = driver.FindElement(By.XPath(("//a[@aria-label='M']/div")));
            size.Click();
            Log.Information("Choosing filter By Size");

        }

        [When(@"selects the product from list")]
        public void WhenSelectsTheProductFromList()
        {
            IWebElement? selectproduct = driver.FindElement(By.XPath(("(//div[@class='product-item-info'])[3]")));
            selectproduct.Click();
            Log.Information("selects product");
        }

        [When(@"User clicks on the add to cart button without choosing required critetia")]
        public void WhenUserClicksOnTheAddToCartButtonWithoutChoosingRequiredCritetia()
        {
            IWebElement addtocart = driver.FindElement(By.Id("product-addtocart-button"));
            addtocart.Click();
            Log.Information("Clicked on add to cart");
        }

        [Then(@"Required criteria error occurs")]
        public void ThenRequiredCriteriaErrorOccurs()
        {
            try
            {
                string msg = driver.FindElement(By.XPath("(//div[@class='mage-error'])[1]")).Text;
                TakeScreenshots(driver);
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
