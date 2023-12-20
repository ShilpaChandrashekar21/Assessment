using Magneto.pageObjects;
using Magneto.utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magneto.testScripts
{
    [TestFixture]
    internal class MagentoHomePageTest : CoreCodes //Inheritance
    {
        [Test,Category("SmokeTest")]
        [TestCase("pants")]
        public void HomePageElementsTest(string input)
        {
            var homePage = new MagentoHomePage(driver);
            homePage.ClickOnLogo();
            ScreenShotTest();
            Assert.That(driver.Url.Equals("https://magento.softwaretestingboard.com/"));
            homePage.SearchBarInput(input);
            homePage.SelectNavBarDropdown();
        }
    }
}
