
using AventStack.ExtentReports;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DeltaHRMS.build2
{
    class ApplyLeave : BasePage
    {
        [Test]
        public void UserShouldBeAbleToApplyLeave()
        {
            test = extent.CreateTest(GetExcelData("Test Case",2));

            var xmldoc = new XmlDocument();
            xmldoc.Load(projectPath +"\\TestData.xml");
            var username = xmldoc.SelectSingleNode("DeltaHRMS//UserName").InnerText;
            var password = xmldoc.SelectSingleNode("DeltaHRMS//Password").InnerText;
            
            LoginPage loginPage = new LoginPage(driver);
            loginPage.SignIn(username,password);
            test.Pass(UpdateExcel("Step Description", test.Status.ToString()), MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

            Navigation nav = new Navigation(driver);
            nav.Goto("Self Service").ChildTab("Leaves", "Leave Request");
            test.Pass(UpdateExcel("Step Description",  test.Status.ToString()), MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

            LeaveRequestPage lrp = new LeaveRequestPage(driver);
            lrp.ClickApply();
            lrp.CreateLeave.SelectLeaveType("Earned Leave");
            test.Pass(UpdateExcel("Step Description",  test.Status.ToString()), MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

            lrp.CreateLeave.SelectDateRange(18, 18);
            test.Pass(UpdateExcel("Step Description", test.Status.ToString()), MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

            lrp.CreateLeave.EnterReason("Test Automation");
            test.Pass(UpdateExcel("Step Description",  test.Status.ToString()), MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

            lrp.CreateLeave.SelectAction("Apply");
            test.Pass(UpdateExcel("Step Description",  test.Status.ToString()), MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

        }

        [Test]
        public void ValidateLeaves()
        {
            test = extent.CreateTest("ValidateLeaves");

            var xmldoc = new XmlDocument();
            xmldoc.Load(projectPath + "\\TestData.xml");
            var username = xmldoc.SelectSingleNode("DeltaHRMS//UserName").InnerText;
            var password = xmldoc.SelectSingleNode("DeltaHRMS//Password").InnerText;

            LoginPage loginPage = new LoginPage(driver);
            loginPage.SignIn(username, password);

            Navigation nav = new Navigation(driver);
            nav.Goto("Self Service").ChildTab("Leaves", "My Leave");

            MyLeavesPage mlp = new MyLeavesPage(driver);
           //int expectedLeaveCount =(int) mlp.ClickLeave("Cancelled Leave");
            int expCancelLeaveCount = (int) mlp.ClickCancelLeave();
            int actualLeaveCount =(int) mlp.GetLeaveCount();
            Console.WriteLine($"actual leave count : {actualLeaveCount}");
            Assert.AreEqual(expCancelLeaveCount, actualLeaveCount, $"expected :{expCancelLeaveCount} is different from actual :{actualLeaveCount}");
            int expRejLeaveCount = (int)mlp.ClickRejectLeave();
            int actualRejLeaveCount = (int)mlp.GetLeaveCount();
            Console.WriteLine($"actual leave count : {actualRejLeaveCount}");
            Assert.AreEqual(expRejLeaveCount, actualRejLeaveCount, $"expected :{expRejLeaveCount} is different from actual :{actualRejLeaveCount}");

        }
    }
}
