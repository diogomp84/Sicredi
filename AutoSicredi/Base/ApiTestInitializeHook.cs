using AutoSicredi.Config;
using TechTalk.SpecFlow;

namespace AutoSicredi.Base
{
    public class ApiTestInitializeHook : Steps
    {
        public void InitializeSettings()
        {
            ConfigApiReader.SetFrameworkSettings();
        }
    }
}
