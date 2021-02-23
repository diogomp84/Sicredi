using OpenQA.Selenium;
using System;

namespace AutoSicredi.Extensions
{
    public static class WebElementExtensions
    {
        public static bool IsElementPresent(this IWebElement element)
        {
            try
            {
                bool ele = element.Displayed;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
