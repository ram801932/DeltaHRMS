using DeltaHRMS.build2.pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DeltaHRMS.build2
{
    class AppraisalTest : BasePage
    {
        [Test]
        public void SelfAppraisalTest()
        {
            test = extent.CreateTest(GetExcelData("Test Case", 2));

            var xmldoc = new XmlDocument();
            xmldoc.Load(projectPath + "\\TestData.xml");
            var username = xmldoc.SelectSingleNode("DeltaHRMS//employee//UserName").InnerText;
            var password = xmldoc.SelectSingleNode("DeltaHRMS//employee//Password").InnerText;

            LoginPage loginPage = new LoginPage(driver);
            loginPage.SignIn(username, password);

            Navigation nav = new Navigation(driver);
            nav.Goto("Appraisals").ChildTab("Self Appraisal");
           // nav.Goto("Self Service").ChildTab("Leaves", "Leave Request");


            SelfAppraisalPage sap = new SelfAppraisalPage(driver);
            sap.SetRatingforSelfAppraisal();

        }
    }
}
