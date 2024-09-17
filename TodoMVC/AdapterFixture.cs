using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoMVC.Browsers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;
using OpenQA.Selenium.Firefox;
using SeleniumExtras.WaitHelpers;

namespace TodoMVC
{
    public class AdapterFixture : IDisposable
    {
        private const int TIME_TO_WAIT_FOR_ELEMENT = 5;
        protected IWebDriver Driver { get; set; }
        protected WebDriverWait Wait { get; set; }
        public Actions actions;
        protected IJavaScriptExecutor js;

        public void Strat(BrowserTypes browser) {
            switch (browser) {
                case BrowserTypes.CHROME:
                    new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                    Driver = new ChromeDriver(chromeOptions);
                    break;

                case BrowserTypes.FIREFOX:
                    new DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.Latest);
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    firefoxOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                    Driver = new FirefoxDriver(firefoxOptions);
                    break;

                case BrowserTypes.EDGE:
                    new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);
                    EdgeOptions edgeOptions = new EdgeOptions();
                    edgeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                    Driver = new EdgeDriver(edgeOptions);
                    break;

                default:
                    Console.WriteLine("Final");
                    break;
            }

            Driver.Manage().Window.Maximize();
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TIME_TO_WAIT_FOR_ELEMENT));
            actions = new Actions(Driver);
            js = (IJavaScriptExecutor)Driver;
        }

        public void GoToUrl(string url) { 
            Driver.Navigate().GoToUrl(url);
        }

        public IWebElement WaitForElementToBeVisible(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement WaitForElementToBeClickable(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }
        public IWebElement WaitForElementToBeClickable(IWebElement element)
        {
            return Wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public IWebElement GetShadowRoot(IWebElement host)
        {
            return (IWebElement)js.ExecuteScript("return arguments[0].shadowRoot", host);
        }

        public IWebElement GetElementUnderShadowRoot(IWebElement host, By locator)
        {
            string criteria = locator.Criteria.Replace("'", @"\'");
            return (IWebElement)js.ExecuteScript($"return arguments[0].shadowRoot.querySelector('{criteria}')", host);
        }

        public IReadOnlyCollection<IWebElement> GetElementsUnderShadowRoot(IWebElement host, By locator)
        {
            string criteria = locator.Criteria.Replace("'", @"\'");
            return (IReadOnlyCollection<IWebElement>)js.ExecuteScript($"return Array.from(arguments[0].shadowRoot.querySelectorAll('{criteria}'))", host);
        }

        public IWebElement FindElement(By locator) {
            return Wait.Until(ExpectedConditions.ElementExists(locator));
        }

        public void Dispose()
        {
            Driver.Dispose();
        }
    }
}
