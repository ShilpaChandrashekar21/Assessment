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
    internal class MagentoHomePage
    {
        IWebDriver? driver;
        public MagentoHomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName, Using =("logo"))]
        [CacheLookup]
        private IWebElement? Logo {  get; set; }

        [FindsBy(How = How.Id , Using = "search")]
        [CacheLookup]
        private IWebElement? SearchBar { get; set; }

        [FindsBy(How = How.XPath, Using = "(//a[@class='level-top ui-corner-all'])[2]")]
        private IWebElement? NavbarDropdown { get; set; }

        [FindsBy(How = How.XPath, Using = "(//a[@class='ui-corner-all'])[1]")]
        private IWebElement? NavbarInsideDropdown { get; set; }

       
        

       



        public void ClickOnLogo()
        {
            Logo?.Click();
        }

        public ProductPage SearchBarInput(string input) 
        {
            SearchBar?.SendKeys(input);
            SearchBar?.SendKeys(Keys.Enter);
            return new ProductPage(driver);

        }

        public void SelectNavBarDropdown()
        {
            Actions actions = new Actions(driver);
            Action mouseHover = () => actions
               .MoveToElement(NavbarDropdown)
               .Build()
               .Perform();
            mouseHover.Invoke();
        }

        public void SelectNavbarInsideDropdown()
        {
            Actions actions = new Actions(driver);
            Action mouseHover = () => actions
               .MoveToElement(NavbarInsideDropdown)
               .Build()
               .Perform();
            mouseHover.Invoke();

        }

        public ProductDisplayPage SelectProductFromDropDown(string input)
        {
            IWebElement? selectProduct = driver.FindElement(By.XPath(("(//li[starts-with(@class,'level2')]/a)"+"["+input+"]")));
            selectProduct?.Click();
            return new ProductDisplayPage(driver);

        }




    }

}
