using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
namespace ContactBook_Selenium_Tests
{
    public class ContactBook_Selenium_Tests
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }


        [Test]
        public void List_contacts_Steve ()
        {
            driver.Navigate().GoToUrl("https://contactbook.kishy.repl.co/contacts");
            var divFirstResult = driver.FindElement(By.XPath("//table[@id='contact1']/tbody/tr/th"));
            divFirstResult.Click();

            var divContactID =
                driver.FindElement(By.XPath("(//td[contains(.,'1')])[1]"));
            var divContactFristName =
                driver.FindElement(By.XPath("(//td[contains(.,'Steve')])[1]"));
            var divContactLastName =
                driver.FindElement(By.XPath("(//td[contains(.,'Jobs')])[1]"));

            Assert.AreEqual("1", divContactID.Text);
            Assert.AreEqual("Steve", divContactFristName.Text);
            Assert.AreEqual("Jobs", divContactLastName.Text);
        }

        [Test]
        public void Search_By_Valid_Keyword()
        {
            driver.Navigate().GoToUrl
                ("https://contactbook.kishy.repl.co/contacts/search");
            var searchField =
                driver.FindElement(By.XPath("//input[contains(@id,'keyword')]"));

            searchField.Click();
            searchField.SendKeys("albert");

            var buttonSearch =
                driver.FindElement(By.XPath("//button[contains(@id,'search')]"));
            buttonSearch.Click();

            var pageHeading = 
                driver.FindElement(By.XPath("/html/body/main/h1"));
            
            var divFirstContactFirstName =
                driver.FindElement(By.XPath("(//td[contains(.,'Albert')])[1]"));
            var divFirstContactLastName =
                driver.FindElement(By.XPath("(//td[contains(.,'Einstein')])[1]"));

            Assert.AreEqual("Contacts Matching Keyword \"albert\"", pageHeading.Text);
            Assert.AreEqual("Albert", divFirstContactFirstName.Text);
            Assert.AreEqual("Einstein", divFirstContactLastName.Text);
        }

        [Test]
        public void Search_By_Non_Valid_Keyword()
        {
            driver.Navigate().GoToUrl("https://contactbook.kishy.repl.co/contacts/search");
            var searchField =
                driver.FindElement(By.XPath("//input[contains(@id,'keyword')]"));
            searchField.Click();
            searchField.SendKeys("invalid2635");

            var buttonSearch =
                driver.FindElement(By.XPath("//button[contains(@id,'search')]"));
            buttonSearch.Click();

            var pageHeading =
                driver.FindElement(By.XPath("/html/body/main/h1"));
            var searchResult =
                driver.FindElement(By.XPath("//div[contains(@id,'searchResult')]"));

            Assert.AreEqual("Contacts Matching Keyword \"invalid2635\"", pageHeading.Text);
            Assert.AreEqual("No contacts found.", searchResult.Text);
        }

        [Test]
        public void Create_New_Contact_Non_Valid_Data()
        {
            driver.Navigate().GoToUrl("https://contactbook.kishy.repl.co/contacts/create");

            var buttonCreateContact =
                driver.FindElement(By.XPath("//button[contains(@id,'create')]"));
            buttonCreateContact.Click();

            var errorMSG =
                driver.FindElement
                (By.XPath("//div[contains(@class,'err')]"));

            Assert.IsTrue(errorMSG.Displayed);
            Assert.AreEqual("Error: First name cannot be empty!", errorMSG.Text);
        }

        [Test]
        public void Create_New_Valid_Contact()
        {
            driver.Navigate().GoToUrl("https://contactbook.kishy.repl.co/contacts/create");
            var firstNameField =
                driver.FindElement(By.XPath("//input[contains(@id,'firstName')]"));
            firstNameField.Click();
            firstNameField.SendKeys("John");

            var lastNameField =
                driver.FindElement(By.XPath("//input[contains(@id,'lastName')]"));
            lastNameField.Click();
            lastNameField.SendKeys("Cena");

            var emailNameField =
                driver.FindElement(By.XPath("//input[contains(@id,'email')]"));
            emailNameField.Click();
            emailNameField.SendKeys("johncena@test.com");

            var buttonCreate =
               driver.FindElement(By.XPath("//button[contains(@id,'create')]"));
            buttonCreate.Click();

            var newContactFirstName =
                driver.FindElement(By.XPath("//td[contains(.,'John')]"));
            var newContactLastName =
                driver.FindElement(By.XPath("//td[contains(.,'Cena')]"));
            var newContactEmail =
                driver.FindElement(By.XPath("//td[contains(.,'johncena@test.com')]"));

            
            Assert.AreEqual("John", newContactFirstName.Text);
            Assert.AreEqual("Cena", newContactLastName.Text);
            Assert.AreEqual("johncena@test.com", newContactEmail.Text);
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}