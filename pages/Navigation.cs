
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaHRMS.build2
{
    class Navigation
    {
        IWebDriver driver;

        public Navigation(IWebDriver driver)
        {
            this.driver = driver;
        }
        public Navigation Goto(string tabName)
        {
            driver.FindElement(By.XPath($"//div[@class='side-menu-thumbnail']//li[text()='{tabName}']")).Click();
            return this;
        }

        public Navigation ChildTab(string tabName, string subChild=null)
        {
            driver.FindElement(By.XPath($"//div[@class='side-menu ']//a[text()='{tabName}']")).Click();
            if (!string.IsNullOrEmpty(subChild)) driver.FindElement(By.LinkText(subChild)).Click();
            return this;
        }
    }
}
