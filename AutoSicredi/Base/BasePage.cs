using AutoSicredi.Extensions;
using AutoSicredi.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AutoSicredi.Base
{
    public abstract class BasePage : Base
    {
        public BasePage(ParallelConfig parellelConfig) : base(parellelConfig)
        {
        }
    }
}
