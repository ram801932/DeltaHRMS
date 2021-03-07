using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaHRMS.build2
{
    class LoginPage
    {
        public IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement UserName_Textbox => driver.FindElement(By.Id("username"));

        IWebElement Password_Textbox => driver.FindElement(By.Id("password"));

        IWebElement Login_Button => driver.FindElement(By.Id("loginsubmit"));

        public void SignIn(string userName, string password)
        {
            UserName_Textbox.SendKeys(userName);
            Password_Textbox.SendKeys(password);
            Login_Button.Click();
        }
    }
}
