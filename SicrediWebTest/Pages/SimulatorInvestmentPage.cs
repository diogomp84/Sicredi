using System.Collections.Generic;
using AutoSicredi.Base;
using AutoSicredi.Extensions;
using AutoSicredi.Model;
using OpenQA.Selenium;

namespace SicrediWebTest.Pages
{
    public class SimulatorInvestmentPage : BasePage
    {
        public SimulatorInvestmentPage(ParallelConfig parallelConfig) : base(parallelConfig)
        {
        }

        private By radioProfile => By.Name("perfil");

        private By textApplyValue => By.Id("valorAplicar");

        private By textInvestValue => By.Id("valorInvestir");

        private By textPeriod => By.Id("tempo");

        private By btnSelect => By.CssSelector("a.btSelect");

        private By btnSimulate => By.CssSelector("li.simular");

        private By lblSaved => By.CssSelector("div.blocoResultadoSimulacao > span > strong");

        private By lblValue => By.CssSelector("div.blocoResultadoSimulacao > span.valor");

        private By tableValueOptions => By.CssSelector("div.maisOpcoes > table > tbody > tr");

        private By tableHeaderOptions => By.CssSelector("div.maisOpcoes > table > thead > tr > th");
        private By radioCompany => By.CssSelector("input[type='radio'][value='paraEmpresa']");

        private By radioPersonal => By.CssSelector("input[type='radio'][value='paraVoce']");
        private By optMonths => By.CssSelector("a[rel = 'M']");
        private By optYears => By.CssSelector("a[rel = 'A']");

        private By labelAppliedValue => By.CssSelector("label#valorAplicar-error");

        private By labelInvestedValue => By.CssSelector("label#valorInvestir-error");

        private By labelPeriodValue => By.CssSelector("label#tempo-error");


        public void FillForm(InvestorType investorType, string amountApplied, string savePerMonth, string savedPeriod, PeriodType periodType)
        {
            if (investorType.Equals(InvestorType.Pessoal))
            {
                _parallelConfig.Driver.ClickBy(radioPersonal);
            }
            else if (investorType.Equals(InvestorType.Empresarial))
            {
                _parallelConfig.Driver.ClickBy(radioCompany);
            }

            _parallelConfig.Driver.SendKeysBy(textApplyValue, amountApplied);
            _parallelConfig.Driver.SendKeysBy(textInvestValue, savePerMonth);
            _parallelConfig.Driver.SendKeysBy(textPeriod, savedPeriod);

            _parallelConfig.Driver.ClickBy(btnSelect);

            if (periodType.Equals(PeriodType.Meses))
            {
                _parallelConfig.Driver.ClickBy(optMonths);
            }
            else if (periodType.Equals(PeriodType.Anos))
            {
                _parallelConfig.Driver.ClickBy(optYears);
            }
        }

        public List<string> GetListMonths()
        {
            List<string> listMonths = new List<string>();
            var trs = _parallelConfig.Driver.FindElements(tableValueOptions);
            foreach (var tr in trs)
            {
                var tds = tr.FindElements(By.TagName("td"));
                var periodMonth = tds[0].Text.Trim();

                listMonths.Add(periodMonth);
            }
            return listMonths;
        }

        public List<string> GetListAmounts()
        {
            List<string> listAmounts = new List<string>();
            var trs = _parallelConfig.Driver.FindElements(tableValueOptions);
            foreach (var tr in trs)
            {
                var tds = tr.FindElements(By.TagName("td"));
                var periodMonth = tds[1].Text.Replace("R$", "").Trim();

                listAmounts.Add(periodMonth);
            }
            return listAmounts;
        }

        public string GetFieldInvestedValue()
        {
            return _parallelConfig.Driver.GetText(labelInvestedValue);
        }

        public string GetFieldMsgPeriod()
        {
            return _parallelConfig.Driver.GetText(labelPeriodValue);
        }


        public string GetFieldMsgAppliedValue()
        {
            return _parallelConfig.Driver.GetText(labelAppliedValue);
        }

        public void SimulateInvestment()
        {
            _parallelConfig.Driver.ClickBy(btnSimulate);
        }
        public string GetInformedPeriodInMonths()
        {
            var fullSentence = _parallelConfig.Driver.GetText(lblSaved);
            var numeral = fullSentence.Split(" ")[0];
            return numeral;
        }

        public string GetTotalSaved()
        {
            return _parallelConfig.Driver.GetText(lblValue);
        }
    }
}
