using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace Appium_ContactBook
{
    public class Tests
    {
        string USERNAME = "kristiyan8";
        string AUTOMATE_KEY = "Hs1mtVxeEtrypTmTCqtq";
        string appAdress = "bs://3d129f183c653501415079d1bc6eab7fcb70ed16";
        private AndroidDriver<AndroidElement> driver;

        [OneTimeSetUp]
        public void SetupRemoteServer()
        {

            AppiumOptions caps = new AppiumOptions();
            caps.PlatformName = "Android";
            caps.AddAdditionalCapability("browserstack.user", USERNAME);
            caps.AddAdditionalCapability("browserstack.key", AUTOMATE_KEY);
            //upload app at https://app-automate.browserstack.com/dashboard/v2/getting-started?source=%27home%27
            // to get the appAddress 
            caps.AddAdditionalCapability("app", appAdress);
            caps.AddAdditionalCapability("project", "First CSharp project");
            caps.AddAdditionalCapability("build", "CSharp Android");
            caps.AddAdditionalCapability("name", "first_test");
            caps.AddAdditionalCapability("device", "Google Pixel 3");
            caps.AddAdditionalCapability("os_version", "9.0");

            driver = new AndroidDriver<AndroidElement>(
               new Uri("http://hub.browserstack.com/wd/hub"), caps);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
        }

        [Test]
        public void First_Test()
        {
            var buttonConnect =
            driver.FindElement(By.Id("contactbook.androidclient:id/buttonConnect"));
            buttonConnect.Click();

            var textField =
                driver.FindElement(By.Id("contactbook.androidclient:id/editTextKeyword"));
            textField.Click();
            textField.SendKeys("Steve");

            var buttonSearch = driver.FindElement(By.Id("contactbook.androidclient:id/buttonSearch"));
            buttonSearch.Click();

            var firstName = driver.FindElement(By.Id("contactbook.androidclient:id/textViewFirstName"));
            var lastName = driver.FindElement(By.Id("contactbook.androidclient:id/textViewLastName"));
            var email = driver.FindElement(By.Id("contactbook.androidclient:id/textViewEmail"));

            Assert.AreEqual("Steve", firstName.Text);
            Assert.AreEqual("Jobs", lastName.Text);
            Assert.AreEqual("steve@apple.com", email.Text);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}