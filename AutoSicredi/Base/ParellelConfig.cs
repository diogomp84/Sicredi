using OpenQA.Selenium.Remote;

namespace AutoSicredi.Base
{
    public class ParallelConfig
    {

        public RemoteWebDriver Driver { get; set; }

        public BasePage CurrentPage { get; set; }
    }
}
