using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magneto.pageObjects
{
    internal class ProductDisplayPage
    {
        IWebDriver driver;
        public ProductDisplayPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "(//div[@class='filter-options-title'])[2]")]
        private IWebElement? FilterBySize { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@aria-label='M']/div")]
        private IWebElement? Size { get; set; }

        [FindsBy(How = How.XPath, Using = "(//div[@class='product-item-info'])[3]")]
        private IWebElement? SelectProduct { get; set; }

       
        [FindsBy(How = How.Id, Using = "product-addtocart-button")]
        private IWebElement? AddToCart { get; set; }
       


        public void ClickOnFilterBySize()
        {
            FilterBySize?.Click();
        }

        public void SelectSize()
        {
            Size?.Click();
        }

        public void SelectProductFromList()
        {
            SelectProduct?.Click();
        }

        public void ClickOnAddToCart()
        {
            AddToCart?.Click();
        }




    }
}
