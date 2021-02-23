using RestSharp;
using System;

namespace AutoSicredi.Config
{
    public class RestSettings
    {
        public IRestResponse Response { get; set; }
        public IRestRequest Request { get; set; }
        public RestClient RestClient { get; set; } = new RestClient();
    }
}
