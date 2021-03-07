using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeltaHRMS.build2
{
    class BasePage
    {
        public static IWebDriver driver;

        public ExtentReports extent;
        public ExtentTest test;
        public int ScreenshotCount;
        public string exceptionMessage;
        public string projectPath;
        public string colName;
        public int cellCount = 2;
        public ExcelHelper xlHelper;

        [OneTimeSetUp]
        public void LaunchBrowser()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            projectPath = Path.GetFullPath(Path.Combine(path, @"..\..\"));
            string reportPath = projectPath + @"Reports\TestReport.html";
            var reporter = new ExtentV3HtmlReporter(reportPath);
            reporter.LoadConfig($"{projectPath}\\extent-config.xml");

            extent = new ExtentReports();
            extent.AttachReporter(reporter);

            extent.AddSystemInfo("Application ", "Delta HRMS");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Machine", Environment.MachineName);
            extent.AddSystemInfo("Operating System", Environment.OSVersion.VersionString);

            xlHelper = new ExcelHelper(projectPath + "Execution Status.xlsx");

            driver = new ChromeDriver(); 
        }

        [SetUp]
        public void Setup()
        {
            driver.Url = ConfigurationManager.AppSettings["URL"];

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void logout()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status.ToString();

            var errorMessage = TestContext.CurrentContext.Result.Message;

            switch (status)
            {
                case "Passed":
                    test.Pass("Testcase is Passed", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());
                    status = "Pass";
                    break;

                case "Skipped":
                    test.Skip("Testcase is Skipped", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());
                    status = "Skip";
                    break;

                case "Failed":
                    test.Fail($"Failed :{errorMessage}", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());
                    status = "Fail";
                    break;
            }

            if (status != "Pass")
            {
                xlHelper.SetCellData("Execution Status", "Step Status", cellCount, status);
                xlHelper.SetCellData("Execution Status", "Step Completion time", cellCount, DateTime.Now.ToString("g"));
            }

            xlHelper.SetCellData("Execution Status", "Test Case status", 2, status);

            driver.FindElement(By.Id("logoutbutton")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();

        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            driver.Quit();
            extent.Flush();
        }
        public string screencapture()
        {
            ScreenshotCount += 1;
            var strPath = $"{projectPath}\\Reports\\screenshot{ScreenshotCount}.png";
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(strPath);
            return strPath;
        }

       
        public string WriteToExcell(string testStep, string status)
        {
            xlHelper.SetCellData("Sheet1", "Test Steps", cellCount, testStep);
            xlHelper.SetCellData("Sheet1", "Status", cellCount, status);
            cellCount++;
            return testStep;
        }

        public string GetExcelData(string colName, int rownum)
        {
            this.colName = colName;
            return xlHelper.GetCellData("Execution Status", colName, rownum);
        }

        public string UpdateExcel(string colName, string status)
        {
            string testStep = xlHelper.GetCellData("Execution Status", colName, cellCount);
            xlHelper.SetCellData("Execution Status", "Step Status", cellCount, status);
            xlHelper.SetCellData("Execution Status", "Step Completion time", cellCount, DateTime.Now.ToString("g"));
            cellCount++;
            return testStep;
        }
    }
}
