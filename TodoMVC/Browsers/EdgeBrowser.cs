using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVC.Browsers
{
    public class EdgeBrowser : DriverFixture
    {
        protected override void InitializeDriver()
        {
            Driver.Value.Strat(BrowserTypes.EDGE);
        }

        public override int WaitForElementTimeout => 30;
    }
}
