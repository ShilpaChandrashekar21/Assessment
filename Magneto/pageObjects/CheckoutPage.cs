using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magneto.pageObjects
{
    internal class CheckoutPage
    {
        IWebDriver? driver;

        public CheckoutPage(IWebDriver? driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[contains(@data-bind,'email') and @id ='customer-email']")]
        private IWebElement? Email {  get; set; }

        [FindsBy(How = How.Name, Using = "firstname")]
        private IWebElement? FirstName { get; set; }
        
        [FindsBy(How = How.Name, Using = "lastname")]
        private IWebElement? LastName { get; set; }
        
        [FindsBy(How = How.Name, Using = "company")]
        private IWebElement? Company { get; set; }
        
        [FindsBy(How = How.Name, Using = "street[0]")]
        private IWebElement? StreetAddress { get; set; }

        [FindsBy(How = How.Name, Using = "city")]
        private IWebElement? City { get; set; }
        
        [FindsBy(How = How.Name, Using = "region_id")]
        private IWebElement? State { get; set; }
        
        [FindsBy(How = How.Name, Using = "postcode")]
        private IWebElement? PostalCode { get; set; }
        
        /*[FindsBy(How = How.Name, Using = "country_id")]
        private IWebElement? Country { get; set; }*/

        [FindsBy(How = How.Name, Using = "telephone")]
        private IWebElement? PhoneNumber { get; set; }

        [FindsBy(How =How.XPath, Using = "//input[@class='radio' and @value='flatrate_flatrate']")]
        private IWebElement? ShippingMethod { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@data-role='opc-continue']")]
        private IWebElement? NextButton { get; set; }

        [FindsBy(How =How.XPath, Using = "//button[@title='Place Order']")]
        private IWebElement? PlaceOrder { get; set; }

        public void SelectState()
        {
            SelectElement selectstate = new SelectElement(State);
            selectstate.SelectByText("Texas");
        }


        public void ShippingDetails(string? email, string? fname,string? lname,string? company,string? address
            ,string? city,string? state, string? zip, string? ph)
        {
            Email?.SendKeys(email);
            FirstName?.SendKeys(fname); 
            LastName?.SendKeys(lname);
            Company?.SendKeys(company);
            StreetAddress?.SendKeys(address);
            City?.SendKeys(city);
            State?.SendKeys(state);
            PostalCode?.SendKeys(zip);
            PhoneNumber?.SendKeys(ph);

        }

        public void SelectShippingMethod()
        {
            ShippingMethod?.Click();

        }
        
        public void ClickOnNextButton()
        {
            NextButton?.Click();
        }
      
        public void CLickOnPlaceOrder()
        {
            PlaceOrder?.Click();
        }
    }
        
}
