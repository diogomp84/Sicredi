using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutoSicredi.Helpers
{
	public static class WebWaitHelpers
	{
		#region Wait Base

		#region Comments

		//
		// Summary:
		//     Utilizado em esperas que tem um tempo mínimo ou máximo para testar, evita consumo de recursos em um looping persistente.
		//
		// Parameters:
		//   interval:
		//     Intervalo entre as tentativas.
		//
		//   timeout:
		//     Tempo maximo de espera.

		#endregion Comments

		public static DefaultWait<T> FluentWait<T>(T webObject, int timeout, double interval)
		{
			var wait = new DefaultWait<T>(webObject)
			{
				Timeout = new TimeSpan(0, 0, timeout),
				PollingInterval = TimeSpan.FromSeconds(interval)
			};

			wait.IgnoreExceptionTypes(typeof(NoSuchElementException),
									  typeof(NoSuchWindowException),
									  typeof(NoSuchFrameException),
									  typeof(StaleElementReferenceException),
									  typeof(NotImplementedException));

			return wait;
		}

		#region Comments

		/// Espera para que algo no driver possa ser utilizado

		#endregion Comments

		public static DefaultWait<IWebDriver> DriverWait(this IWebDriver driver, int timeout = 60, double interval = 0.4)
		{
			return FluentWait(driver, timeout, interval);
		}

		#region Comments

		/// Espera para quando o elemento já é conhecido mas é necessario aguardar por algum motivo com base nele

		#endregion Comments

		public static DefaultWait<IWebElement> ElementWait(this IWebElement element, int timeout = 60, double interval = 0.4)
		{
			return FluentWait(element, timeout, interval);
		}

		#endregion Wait Base

		#region Comments

		/// Nas condições os retornos null, false ou exceptions ignorados fazem permanescer no looping até retornar algo diferente 

		#endregion Comments

		#region Conditions

		public static Func<IWebElement, IWebElement> WaitElementIsVisible(By locator)
		{
			return (element) =>
			{
				try
				{
					var subElement = element.FindElement(locator);

					return subElement.Displayed ? subElement : null;
				}
				catch (StaleElementReferenceException)
				{
					return null;
				}
			};
		}

		public static Func<IWebElement, bool> WaitElementIsNotOverlaidPerformClick()
		{
			return (element) =>
			{
				try
				{
					element.Click();

					return true;
				}
				catch (ElementClickInterceptedException)
				{
					return false;
				}
			};
		}

		public static Func<IWebElement, bool> WaitTextFieldValueChange(string oldValue)
		{
			return element =>
			{
				if (element.GetAttribute("value") != oldValue)
				{
					return true;
				}

				return false;
			};
		}

		public static Func<IWebDriver, string> WaitWindowContainsTitlePerformSwitch(string title)
		{
			return driver =>
			{
				foreach (var window in driver.WindowHandles)
				{
					driver.SwitchTo().Window(window);

					if (driver.Title.Contains(title))
					{
						return window;
					}
				}

				throw new NoSuchWindowException();
			};
		}
		#endregion Conditions
	}
}
