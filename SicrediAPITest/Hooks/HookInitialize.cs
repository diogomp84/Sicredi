using AutoSicredi.Base;
using AutoSicredi.Config;
using TechTalk.SpecFlow;

namespace SicrediAPITest.Hooks
{
    [Binding]
    public class HookInitialize : ApiTestInitializeHook
    {
        private readonly RestSettings _settings;

        public HookInitialize(RestSettings settings)
        {
            _settings = settings;
        }

        [BeforeScenario]
        public void Initialize()
        {
            InitializeSettings();
        }

    }
}
