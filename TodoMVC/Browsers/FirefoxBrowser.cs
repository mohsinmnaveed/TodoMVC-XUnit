using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVC.Browsers
{
    public class FirefoxBrowser : DriverFixture
    {
        protected override void InitializeDriver()
        {
            Driver.Value.Strat(BrowserTypes.FIREFOX);
        }

        public override int WaitForElementTimeout => 60;
    }
}
