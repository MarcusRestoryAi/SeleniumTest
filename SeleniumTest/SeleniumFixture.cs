using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest
{
    public class SeleniumFixture : IDisposable
    {

        public ChromeDriver chromeDriver { get; set; }

        public SeleniumFixture()
        {
            chromeDriver = new ChromeDriver();
        }

        public void Dispose()
        {
            chromeDriver.Quit();
            chromeDriver.Dispose();
        }
    }
}
