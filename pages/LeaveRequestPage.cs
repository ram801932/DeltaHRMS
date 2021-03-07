using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaHRMS.build2
{
    class LeaveRequestPage
    {
        IWebDriver driver;

        private CreateLeaveRequestModalClass _createLeaveRequest = null;

        IWebElement Apply_Button => driver.FindElement(By.ClassName("apply_button"));
       
        public LeaveRequestPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickApply()
        {
            Apply_Button.Click();
        }

       

        public CreateLeaveRequestModalClass CreateLeave => _createLeaveRequest != null ? _createLeaveRequest : _createLeaveRequest = new CreateLeaveRequestModalClass(driver);
        public class CreateLeaveRequestModalClass
        {
            IWebDriver driver;

            IWebElement UI_Select_LeaveType_Button => driver.FindElement(By.XPath("//span[text()='Select Leave Type']"));

            IWebElement UI_From_Date_Element => driver.FindElement(By.Id("from_date"));

            IWebElement UI_To_Date_Element => driver.FindElement(By.Id("to_date"));

            IWebElement UI_Reason_Textbox => driver.FindElement(By.Id("reason"));

            IWebElement UI_Apply_Button => driver.FindElement(By.Id("submitbutton"));
            
            IWebElement UI_Cancel_Button => driver.FindElement(By.Id("Canceldialog"));
            IWebElement UI_Modal => driver.FindElement(By.Id("leaverequestform"));


            public CreateLeaveRequestModalClass(IWebDriver driver)
            {
                this.driver = driver;
            }

            public void SelectLeaveType(string leaveType)
            {
                UI_Select_LeaveType_Button.Click();
                driver.FindElement(By.XPath($"//span[contains(.,'{leaveType}')]")).Click();
            }

            public void SelectDateRange(int fromDate,int toDate)
            {
                UI_From_Date_Element.Click();
                driver.FindElement(By.XPath($"//table[@class='ui-datepicker-calendar'] /tbody/tr/td/a[text()='{fromDate}']")).Click();
                UI_To_Date_Element.Click();
                driver.FindElement(By.XPath($"//table[@class='ui-datepicker-calendar'] /tbody/tr/td/a[text()='{toDate}']")).Click();

            }

            public void EnterReason(string text)
            {
               // BasePage.WaitForNotExist(By.XPath("(//img[@id='img - spinner'])[2]"), 30);

                UI_Reason_Textbox.SendKeys(text);
            }

            public void SelectAction(string option)

            {
                switch (option)
                {
                    case "Cancel":
                        UI_Cancel_Button.Click();
                        break;

                    case "Apply":
                        UI_Apply_Button.Click();
                        break;

                    default:
                        throw new Exception("Invalid option");
                }
                ElementHelper.WaitForNotExist(UI_Modal);
            }
        }
    }
}
