using Magneto.utilites;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magneto.pageObjects
{
    internal class ProductPage
    {
        IWebDriver? driver;
        public ProductPage(IWebDriver driver ) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id,Using = "sorter")]
        private IWebElement? SortBy { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'text') and @index ='1']")]
       
        private IWebElement? SelectSize {  get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'color') and @index ='0']")]
        private IWebElement? SelectColor { get; set; }

        [FindsBy(How = How.Id, Using = "product-addtocart-button")]
        private IWebElement? AddToCart { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(@class,'showcart')]")]
        private IWebElement? CartButton { get; set; }

        [FindsBy(How =How.Id, Using = "top-cart-btn-checkout")]
        private IWebElement? ProceedToCheckout { get; set; }

       

        public void SelectSortBy()
        {
            SelectElement selectsort = new SelectElement(SortBy);
            selectsort.SelectByValue("price");
        }

        public void SelectProduct(string position)
        {
            IWebElement product = driver.FindElement(By.XPath("(//img[@class='product-image-photo'])" + "[" + position + "]"));
            product.Click();
           
        }
        
        public void ChooseSize()
        {
            SelectSize?.Click();
            
        }

        public void ChooseColor()
        {
            SelectColor?.Click();
        }
        
        public void ClickOnAddToCartButton()
        {
            AddToCart?.Click();
        }

        public void ClickOnCart()
        {
            CartButton?.Click();
        }

        public CheckoutPage ClickOnProceedToCheckout()
        {
              
            ProceedToCheckout?.Click();

            return new CheckoutPage(driver);
        }
    }
}
