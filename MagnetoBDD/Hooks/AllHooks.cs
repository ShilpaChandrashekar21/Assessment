using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Serilog;
using TechTalk.SpecFlow;

namespace MagnetoBDD.Hooks
{
    [Binding]
    public sealed class AllHooks
    {
        public static IWebDriver? driver;

        [BeforeFeature]
        public static void InitializeBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [BeforeFeature]
        public static void CreatingLog()
        {
            string? curDir = Directory.GetParent(@"../../../").FullName;
            string? fileName = curDir + "/Logs/logs_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
                 .WriteTo.Console()
                 .WriteTo.File(fileName, rollingInterval: RollingInterval.Day)
                 .CreateLogger();

        }

        [AfterScenario]
        public static void NavigateBack()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            Log.CloseAndFlush();

        }
        [AfterFeature]
        public static void CloseBrowser()
        {
            driver?.Close();
        }

    }
}