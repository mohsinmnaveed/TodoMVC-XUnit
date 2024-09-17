using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVC.Pages
{
    public class PagesFixture : IDisposable
    {
        public PagesFixture()
        {
            HomePage = new HomePage(DriverFixture.Driver.Value);
            TodoPage = new TodoPage(DriverFixture.Driver.Value);
        }

        public HomePage HomePage { get; set; }
        public TodoPage TodoPage { get; set; }

        public void Dispose()
        {
        }
    }
}
