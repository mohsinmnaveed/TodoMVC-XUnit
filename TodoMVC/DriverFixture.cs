using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoMVC.Browsers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace TodoMVC
{
    public abstract class DriverFixture : IDisposable
    {
        public DriverFixture()
        {
            Driver = new ThreadLocal<AdapterFixture>(() => new AdapterFixture());
            InitializeDriver();
        }

        private const int TIME_TO_WAIT_FOR_ELEMENT = 10;
        public static ThreadLocal<AdapterFixture>? Driver { get; set; }
        protected abstract void InitializeDriver();
        public virtual int WaitForElementTimeout { get; set; } = TIME_TO_WAIT_FOR_ELEMENT;

        public void Dispose()
        {
            Driver.Value.Dispose();
        }
    }
}
