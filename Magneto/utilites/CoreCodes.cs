using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Magneto.utilites
{
    internal class CoreCodes
    {
       public  IWebDriver? driver;

        public ExtentReports extent;
        ExtentSparkReporter sparkReporter;
        public ExtentTest test;

        [OneTimeSetUp]
        public void InitializeBrowser()
        {
           ReadConfigFile.ReadConfigSettings();
            string currDir = Directory.GetParent(@"../../../").FullName;

            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currDir + "/extentReports/extent-report"
                + DateTime.Now.ToString("dd_MM_yyyy_HH-mms-s") + ".html");

            extent.AttachReporter(sparkReporter);

            if (ReadConfigFile.properties["browser"].ToLower() == "chrome")
            {
                driver = new ChromeDriver();
            }
            else if (ReadConfigFile.properties["browser"].ToLower() == "edge")
            {
                driver = new EdgeDriver();
            }

            driver.Url = ReadConfigFile.properties["baseUrl"];
            driver.Manage().Window.Maximize();



        }

       

       public void CreateLogs()
        {
            string? curDir = Directory.GetParent(@"../../../").FullName;
            string filename = curDir + "/logs/logs_" + DateTime.Now.ToString("dd_MM_yyyy_hhmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(filename, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void ScreenShotTest()
        {
            ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
            Screenshot screenshot = takesScreenshot.GetScreenshot();

            string? curDir = Directory.GetParent(@"../../../").FullName;
            string filename = curDir + "/screenShots/ss_" + DateTime.Now.ToString("dd_mm_yyyy_hhmmss") + ".png";
            screenshot.SaveAsFile(filename);
        }

        public static void ScrollIntoView(IWebDriver driver, IWebElement element)
        {

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true); ", element);
        }

        protected void LogTestResult(string testName, string result, string errorMessage = null)
        {
            Log.Information(result);
            test = extent.CreateTest(testName);
            if (errorMessage == null)
            {
                Log.Information(testName + " Passed");
                test.Pass(result);

            }
            else
            {
                Log.Error($"Test failed for {testName} \n Exception:\n{errorMessage}");
                test.Fail(result);
            }

        }


        [OneTimeTearDown]
        public void Cleanup()
        {
            driver.Quit();
            extent.Flush();
        }
    }
}
