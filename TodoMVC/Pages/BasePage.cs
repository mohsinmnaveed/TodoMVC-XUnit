using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVC.Pages
{
    public class BasePage
    {
        private readonly AdapterFixture _fixture;

        public BasePage(AdapterFixture fixture)
        {
            _fixture = fixture;
        }


    }
}
