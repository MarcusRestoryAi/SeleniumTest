using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V123.DOM;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;

namespace SeleniumTest
{
    public class Selenium : IClassFixture<SeleniumFixture>
    {

        SeleniumFixture driver;
        ITestOutputHelper output;

        public Selenium(SeleniumFixture webDriver, ITestOutputHelper helper)
        {
            driver = webDriver;
            output = helper;
        }

        [Fact]
        public void FormAuthenticationSuccessTest()
        {
            ChromeDriver chromeDriver = driver.chromeDriver;
            /*
            chromeDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/");

            //Navigera till Form Authentication
            chromeDriver.FindElement(By.LinkText("Form Authentication")).Click();
            */

            GoToUrlAndLink(chromeDriver, "Form Authentication");

            //Mata in värden till InputFält
            //Thread.Sleep(2000);
            chromeDriver.FindElement(By.Id("username")).SendKeys("tomsmith");
            //Thread.Sleep(2000);
            chromeDriver.FindElement(By.Id("password")).SendKeys("SuperSecretPassword!");
            //Thread.Sleep(2000);
            chromeDriver.FindElement(By.XPath("//button[@class ='radius']")).Click();

            //Jämför h2 taggen för testen Secure Area
            Assert.Equal("Secure Area", chromeDriver.FindElement(By.TagName("h2")).Text);
            //Thread.Sleep(2000);

            //Logga ut
            chromeDriver.FindElement(By.CssSelector(".button")).Click();

            //Jämför utloggningsmeddelande
            Assert.Equal("You logged out of the secure area!\r\n×", chromeDriver.FindElement(By.Id("flash")).Text);
        }

        [Fact]
        public void FormAuthenticationFailTest()
        {
            ChromeDriver chromeDriver = driver.chromeDriver;
            GoToUrlAndLink(chromeDriver, "Form Authentication");

            //Mata in värden till InputFält
            chromeDriver.FindElement(By.Id("username")).SendKeys("hello");
            chromeDriver.FindElement(By.Id("password")).SendKeys("world");
            chromeDriver.FindElement(By.XPath("//button[@class ='radius']")).Click();

            //Jämför h2 taggen för testen Secure Area
            Assert.Equal("Your username is invalid!\r\n×", chromeDriver.FindElement(By.Id("flash")).Text);
        }

        [Fact]
        public void BigAndSmallWindow()
        {
            ChromeDriver chromeDriver = driver.chromeDriver;
            GoToUrlAndLink(chromeDriver, "Form Authentication");

            //Maximize
            chromeDriver.Manage().Window.Maximize();
            //Thread.Sleep(1000);

            //Minimize
            chromeDriver.Manage().Window.Minimize();
            //Thread.Sleep(1000);

            //Maximize
            chromeDriver.Manage().Window.Maximize();
            //Thread.Sleep(1000);

            //Minimize
            chromeDriver.Manage().Window.Minimize();
            //Thread.Sleep(1000);
        }

        [Fact]
        public void Dropdown()
        {
            ChromeDriver chromeDriver = driver.chromeDriver;
            GoToUrlAndLink(chromeDriver, "Dropdown");

            //Hämta Dropdown Boxen
            IWebElement dropdown = chromeDriver.FindElement(By.Id("dropdown"));

            //Skapa ett Select komponent
            SelectElement element = new SelectElement(dropdown);

            //Markera Option 1
            element.SelectByIndex(1);
            Assert.Equal("Option 1", element.SelectedOption.Text);
            
            //Markera Option 2
            element.SelectByValue("2");
            Assert.Equal("Option 2", element.SelectedOption.Text);


        }

        [Fact]
        public void Checkboxes()
        {
            ChromeDriver chromeDriver = driver.chromeDriver;
            GoToUrlAndLink(chromeDriver, "Checkboxes");

            //Hämta ref till CheckBox 1
            IWebElement checkbox1 = chromeDriver.FindElement(By.XPath("html/body/div[2]/div/div/form/input[1]"));
            IWebElement checkbox2 = chromeDriver.FindElement(By.XPath("html/body/div[2]/div/div/form/input[2]"));

            checkbox1.Click();
            checkbox2.Click();
        }

        [Fact]
        public void DynamicLoading()
        {
            ChromeDriver chromeDriver = driver.chromeDriver;
            GoToUrlAndLink(chromeDriver, "Dynamic Loading");

            //Gå till Exempel 2 och klicka på start-knappen
            chromeDriver.FindElement(By.LinkText("Example 2: Element rendered after the fact")).Click();
            chromeDriver.FindElement(By.CssSelector("button")).Click();

            //Wait for Dynamic loading
            int timeout = 10;
            WebDriverWait wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(timeout));
            IWebElement loadedText = wait.Until(chromeDriver => chromeDriver.FindElement(By.XPath("//h4[contains(text(), 'Hello World!')]")));

            //Assert
            //Assert.Equal("Hello World!", chromeDriver.FindElement(By.XPath("h4[contains(text(), 'Hello World!')]")).Text);
            Assert.Equal("Hello World!", loadedText.Text);

            


        }

        private void GoToUrlAndLink(ChromeDriver chromeDriver, string link)
        {
            chromeDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/");
            chromeDriver.FindElement(By.LinkText(link)).Click();
        }
    }
}