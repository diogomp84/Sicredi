using AutoSicredi.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace AutoSicredi.Extensions
{
    public static class WebDriverExtensions
    {
        public static string GetText(this IWebDriver driver, By locator, int timeout = 120, double interval = 0.4)
        {
            return driver.DriverWait(timeout, interval).Until(ExpectedConditions.ElementIsVisible(locator)).Text;
        }

        public static void SendKeysBy(this IWebDriver driver, By locator, string txt, int timeout = 120, double interval = 0.4, bool clearBefore = false)
        {
            var element = driver.DriverWait(timeout, interval).Until(ExpectedConditions.ElementIsVisible(locator));

            if (clearBefore) element.Clear();

            if (string.IsNullOrEmpty(txt)) return;

            try
            {
                var hasMask = (bool)(driver as RemoteWebDriver).ExecuteScript("return ('unmask' in $._data(arguments[0], 'events'));", element);

                if (hasMask)
                {
                    (driver as RemoteWebDriver).ExecuteScript($"arguments[0].value='{txt}';", element);
                    (driver as RemoteWebDriver).ExecuteScript("arguments[0].dispatchEvent(new Event('input'));", element);

                    return;
                }
            }
            catch { }

            element.SendKeys(txt);
        }

        public static void ClickBy(this IWebDriver driver, By locator, int timeout = 120, double interval = 0.4)
        {
            driver.DriverWait(timeout, interval).Until(ExpectedConditions.ElementIsVisible(locator)).Click();
        }

        public static void WaitElement(this IWebDriver driver, By locator, int timeout = 120, double interval = 0.4)
        {
            driver.DriverWait(timeout, interval).Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static void SelectDropDownListByText(this IWebDriver driver, By locator, string text, int timeout = 120, double interval = 0.4)
        {
            if (string.IsNullOrEmpty(text)) return;

            var element = driver.DriverWait(timeout, interval).Until(ExpectedConditions.ElementIsVisible(locator));

            new OpenQA.Selenium.Support.UI.SelectElement(element).SelectByText(text);
        }

        public static void HighlightElement(this IWebDriver driver, IWebElement element)
        {
            var jsDriver = (IJavaScriptExecutor)driver;
            string highlightJavascript = @"$(arguments[0]).css({ ""border-width"" : ""2px"", ""border-style"" : ""solid"", ""border-color"" : ""red"", ""background"" : ""yellow"" });";
            jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
        }
        public static void ForceClickByJS(this IWebDriver driver, IWebElement element)
        {
            var jsDriver = (IJavaScriptExecutor)driver;
            jsDriver.ExecuteScript("arguments[0].click();", element);
        }

        public static void WaitUntilAttributeValueEquals(this IWebDriver driver, IWebElement webElement, string attributeName, string attributeValue)
        {
            OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(60));

            wait.Until<IWebElement>((d) =>
            {
                if (webElement.GetAttribute(attributeName) == attributeValue)
                {
                    return webElement;
                }
                return null;
            });
        }

        public static void ForceClick(this IWebDriver driver, IWebElement element, int attempts)
        {
            for (int i = 0; i < attempts; i++)
            {
                try
                {
                    element.Click();
                    break;
                }
                catch (ElementClickInterceptedException)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
