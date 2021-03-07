using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeltaHRMS.build2
{
    class MyLeavesPage
    {
        IWebDriver driver;

        IWebElement Pending_Leaves_Button => driver.FindElement(By.Id("filter_pendingleaves"));

        IWebElement Rejected_Leaves_Button => driver.FindElement(By.Id("filter_rejectedleaves"));

        IWebElement Cancel_Leaves_Button => driver.FindElement(By.Id("filter_cancelleaves"));

        public MyLeavesPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public double ClickCancelLeave()
        {
            double leaveCount = 0;
            Cancel_Leaves_Button.Click();
            var leaveCountText = Cancel_Leaves_Button.FindElement(By.TagName("label")).Text;
            if (!string.IsNullOrEmpty(leaveCountText))
            {
                leaveCount = Convert.ToDouble(leaveCountText);
            }
            return Convert.ToDouble(leaveCount);
        }


        public double ClickRejectLeave()
        {
            double leaveCount = 0;
            Rejected_Leaves_Button.Click();
            var leaveCountText = Rejected_Leaves_Button.FindElement(By.TagName("label")).Text;
            if (!string.IsNullOrEmpty(leaveCountText))
            {
                leaveCount = Convert.ToDouble(leaveCountText);
            }
            return Convert.ToDouble(leaveCount);
        }

        public double ClickLeave(string leave)
        {
            double leaveCount = 0;
            IWebElement element = null;

            switch (leave)
            {
                case "Rejected Leave":
                    element = Rejected_Leaves_Button;
                    break;
                case "Cancelled Leave":
                    element = Cancel_Leaves_Button;
                    break;
                default:
                    throw new Exception("Invalid option");
            }
            element.Click();

            var leaveCountText = element.FindElement(By.TagName("label")).Text;
            if (!string.IsNullOrEmpty(leaveCountText))
            {
                leaveCount = Convert.ToDouble(leaveCountText);
            }
            return leaveCount;
        }

        public double GetLeaveCount()
        {
            double leaveCount = 0;

            while (true)
            {
                leaveCount += LeavesTable();

                //scroll down the page
                IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
                js.ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");

                if (ElementHelper.IsElementExist(By.XPath("//a[@class='nextNew']"), driver))
                {
                    var nextpage = driver.FindElement(By.XPath("//a[@class='nextNew']"));
                    nextpage.Click();
                    Thread.Sleep(3000);
                }
                else 
                    break;
                
            }
            return leaveCount;
        }

        public double LeavesTable()
        {
            double leaveCount = 0;
             
            var tableBody = driver.FindElement(By.XPath("//div[@id='pendingleaves']/table/tbody"));
            var tableRows = tableBody.FindElements(By.TagName("tr"));
            if (ElementHelper.IsElementExist(By.XPath("//td[@class='no-data-td']"), driver))
            {
                return leaveCount;
            }
            for (int i = 1; i < tableRows.Count; i++)
            {
                var tableData = tableRows[i].FindElements(By.TagName("td"))[5].Text;

                leaveCount += Convert.ToDouble(tableData);
            }

            return leaveCount;
        }
    }
}
