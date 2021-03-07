using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeltaHRMS.build2.pages
{
    class SelfAppraisalPage
    {
        IWebDriver driver;

        IList<IWebElement> Tab_List => driver.FindElements(By.XPath("//ul[@role='tablist']/li"));
        IWebElement star => driver.FindElement(By.XPath("//div[@id='rateit-range-2']/div[@class='rateit-selected']"));

        public SelfAppraisalPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void SetRatingforSelfAppraisal()
        {
            //for (int i = 0; i < Tab_List.Count; i++)
            //{
            //    IJavaScriptExecutor jse = (IJavaScriptExecutor) driver;
            //    Tab_List[i].Click();
            //    var rows = driver.FindElements(By.XPath($"//div[contains(@class,'employee_appraisal_tabs')]/div[@aria-labelledby='ui-id-{i+1}']/table/tbody/tr"));
            //    for (int j = 0; j < rows.Count; j++)
            //    {
            //        if (rows[j].FindElements(By.XPath("td")).Count > 1)
            //        {
            //            var star1 = rows[j].FindElement(By.XPath("td[2]//div[@class='rateit-selected']"));
            //            //jse.ExecuteScript("arguments[0].scrollIntoView(); ", star1);
            //            jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            //            Actions act = new Actions(driver);
            //            act.ClickAndHold(star1).MoveByOffset(95, 0).Release().Build().Perform();
            //            Thread.Sleep(300);
            //            rows[j].FindElement(By.XPath("td[3]//textarea")).SendKeys("Test Comments");
            //        }
            //    }
            //}

            for (int i = 0; i < 5; i++)
            {
                var star1 = driver.FindElement(By.XPath($"//div[contains(@class,'employee_appraisal_tabs')]/div[@aria-labelledby='ui-id-1']/table/tbody/tr[{i+1}]/td[2]//div[@class='rateit-selected']"));
                Actions act = new Actions(driver);
                act.ClickAndHold(star1).MoveByOffset(95, 0).Release().Build().Perform();
            }

        }
    }
}
