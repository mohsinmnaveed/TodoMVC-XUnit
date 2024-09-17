using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVC.Pages
{
    public class HomePage
    {
        private readonly AdapterFixture _fixture;
        private string _url = "https://todomvc.com/";
        public HomePage(AdapterFixture fixture)
        {
            _fixture = fixture;
        }

        #region Locators

        By _tabButtonBy;
        IWebElement _tabButton;

        public void SetTechButton(string tab) {
            _tabButtonBy = By.XPath($"//paper-tab/div[text()='{tab}']");
            _tabButton = _fixture.WaitForElementToBeClickable(_tabButtonBy);
        }

        By _appLinkBy;
        IWebElement _appLink;

        public void SetAppLink(string app)
        {
            _appLinkBy = By.XPath($"//span[text()='{app}']");
            _appLink = _fixture.WaitForElementToBeClickable(_appLinkBy);
        }


        #endregion

        public void GotoTodoMVC()
        {
            _fixture.GoToUrl(_url);
        }

        public void ClickTab(string tab) { 
            SetTechButton(tab);
            _tabButton.Click();
        }

        public void Clickapp(string app)
        {
            SetAppLink(app);
            _appLink.Click();
        }


    }
}
