using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeltaHRMS.build2
{
    public static class ElementHelper
    {

        public static bool IsElementExist(By locator,IWebDriver driver)
        {
            bool isExist = false;
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    var element = driver.FindElement(locator);

                    if (element.Displayed || element.Enabled)
                    {
                        isExist = true;
                    }
                    Thread.Sleep(500);
                }
               
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return isExist;
        }

        public static void WaitForNotExist(IWebElement element, int timeoutSec=30)
        {
            bool notExists = false;
            try
            {
                var endTime = DateTime.Now.AddSeconds(timeoutSec);

                while (DateTime.Now.CompareTo(endTime) < 0)
                {
                    if (element == null || !element.Displayed || !element.Enabled)
                    {
                        notExists = true;
                        break;
                    }
                    Thread.Sleep(500);
                }
            }
            catch (Exception)
            {
                return;
            }
            if (!notExists)
            {
                throw new Exception($"WaitForNotExist: Element still exists after {timeoutSec} seconds");
            }

        }

    }
}
