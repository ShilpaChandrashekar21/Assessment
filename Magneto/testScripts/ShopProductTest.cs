using Magneto.pageObjects;
using Magneto.utilites;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Serilog;

namespace Magneto.testScripts
{
    [TestFixture]
    internal class ShopProductTest : CoreCodes
    {
        [Test, Category("E2E testing")]
        public void BuyProductTest()
        {
            CreateLogs();

            var homePage = new MagentoHomePage(driver);


            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/testData/InputData.xlsx";
            string? sheetName = "Products";

            List<Products> excelDataList = ExcelUtils.ReadExcelData(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {
                string? searchtext = excelData?.ProductName;
                string? productposition = excelData?.ProductPosition;


                var productPage = homePage.SearchBarInput(searchtext);
                Log.Information("Searching for product", searchtext);

                try
                {
                    ScreenShotTest();
                    Assert.That(driver.FindElement(By.ClassName("base")).Text.Contains(searchtext));
                    LogTestResult("Searching product ", "Searching product - success");
                }
                catch(AssertionException ex)
                {
                    LogTestResult("Searching product ", $"Searching product - failed \n Exception: {ex.Message}");

                }

                productPage.SelectSortBy();
                Log.Information("Sorting by price");

                string? product = driver.FindElement(By.XPath("(//a[@class='product-item-link'])" + "[" + productposition + "]")).Text;
               // Console.WriteLine(product);


                productPage.SelectProduct(productposition);
                Log.Information("Selecting product");
                try
                {
                    ScreenShotTest();
                    Assert.That(product.Equals(driver.FindElement(By.ClassName("base")).Text));
                  
                    LogTestResult("Selected product ", "Selected product - success");
                }
                catch (AssertionException ex)
                {
                    LogTestResult("Selected product ", $"Selected product - failed \n Exception: {ex.Message}");

                }

                productPage.ChooseSize();
                Log.Information("Choosing size");

                productPage.ChooseColor();
                Log.Information("Choosing color");

                productPage.ClickOnAddToCartButton();
                Log.Information("Clicked on add to cart button");

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                productPage.ClickOnCart();
                Log.Information("Clicked on  cart button");

                DefaultWait<IWebDriver> fwait = new DefaultWait<IWebDriver>(driver);
                fwait.Timeout = TimeSpan.FromSeconds(10);
                fwait.PollingInterval = TimeSpan.FromMilliseconds(150);
                fwait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fwait.Message = "No Element Found";

                 fwait.Until(d => d.FindElement(By.Id("top-cart-btn-checkout")));
               

                var checkoutPage = productPage.ClickOnProceedToCheckout();
                Log.Information("Clicked on Procced to checkout button");
                try
                {

                    ScreenShotTest();
                    Assert.That(driver.Url.Contains("checkout"));

                    LogTestResult("Checkout product ", "Checkout product - success");
                }
                catch (AssertionException ex)
                {
                    LogTestResult("Checkout product ", $"Checkout product - failed \n Exception: {ex.Message}");

                }

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                string? excelSheet = "Shipping Details";

                List<ShippingData> excelShippingDataList = ShippingExcelUtil.ReadShippingExcelData(excelFilePath, excelSheet);

                foreach (var excelShipData in excelShippingDataList)
                {
                    string? email = excelShipData?.Email;
                    string? fName = excelShipData?.FirstName;
                    string? lName = excelShipData?.LastName;
                    string? companyName = excelShipData?.Company;
                    string? address = excelShipData?.Address;
                    string? city = excelShipData?.City;
                    string? state = excelShipData?.State;
                    string? pCode = excelShipData?.PostalCode;
                    string? pNum = excelShipData?.PhoneNumber;
                    checkoutPage.ShippingDetails(email, fName, lName, companyName, address, city, state, pCode, pNum);
                    Log.Information("Entered shipping details");
                }
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                checkoutPage.SelectShippingMethod();
                Log.Information("Selected shipping Method");

                checkoutPage.ClickOnNextButton();
                Log.Information("Clicked on next button");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@title='Place Order']")));

                
                checkoutPage.CLickOnPlaceOrder();
                Log.Information("Clicked on place order button");
                try
                {
                    ScreenShotTest();
                    Assert.That(driver.Url.Contains("payment"));

                    LogTestResult("Placed Order  ", "Placed Order - success");
                }
                catch (AssertionException ex)
                {
                    LogTestResult("Placed Order ", $"Placed Order - failed \n Exception: {ex.Message}");

                }
            }

            
        }

        
    }
}
