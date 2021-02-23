using AutoSicredi.Base;
using AutoSicredi.Config;
using AutoSicredi.Model;
using SicrediWebTest.Pages;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoSicredi.Extensions;

namespace SicrediWebTest.Steps
{
    [Binding]
    public class SimulatorSteps : BaseStep
    { 
        //Context injection
        private new readonly ParallelConfig _parallelConfig;

        public SimulatorSteps(ParallelConfig parallelConfig) : base(parallelConfig)
        {
            _parallelConfig = parallelConfig;
        }

        [When(@"Eu realizo a simulação:")]
        public void QuandoEuRealizoASimulacao(Table table)
        {
            var simulation = table.CreateInstance<(InvestorType investorType, string amountApplied, string savePerMonth, string savedPeriod, PeriodType periodType)> ();
            
            _parallelConfig.CurrentPage.As<SimulatorInvestmentPage>().FillForm(simulation.investorType, simulation.amountApplied, simulation.savePerMonth, simulation.savedPeriod, simulation.periodType);
            _parallelConfig.CurrentPage.As<SimulatorInvestmentPage>().SimulateInvestment();
        }

        [Then(@"vejo que em (.*) meses teria guardado (.*)")]
        public void EntaoVejoQueEmMesesTeriaGuardadoR(string expectedPeriodInMonths, string expectedTotalSaved)
        {
            var actualPeriodInMonths = _parallelConfig.CurrentPage.As<SimulatorInvestmentPage>().GetInformedPeriodInMonths();
            var actualTotalSaved = _parallelConfig.CurrentPage.As<SimulatorInvestmentPage>().GetTotalSaved();

            Assert.AreEqual(expectedPeriodInMonths, actualPeriodInMonths);
            Assert.AreEqual(expectedTotalSaved, actualTotalSaved);
        }

        [Then(@"faço um teste (.*)")]
        public void EntaoFacoUmTeste(List<string> teste)
        {
            var ds = teste.Count;
        }

        [Then(@"vejo outras opções de rentabilidade projetada (.*) \(Meses\) X R\$ (.*)")]
        public void EntaoVejoOutrasOpcoesDeRentabilidadeProjetadaMesesXR_(string months, string amounts)
        {
            var expectedListMonths = DataExtensios.ConvertToList(months);
            var expectedListAmounts = DataExtensios.ConvertToList(amounts);

            var actualListMonths = _parallelConfig.CurrentPage.As<SimulatorInvestmentPage>().GetListMonths();
            var actualListAmounts = _parallelConfig.CurrentPage.As<SimulatorInvestmentPage>().GetListAmounts();

            Assert.IsTrue(expectedListMonths.SequenceEqual(actualListMonths));
            Assert.IsTrue(expectedListAmounts.SequenceEqual(actualListAmounts));

        }

        [Then(@"vejo que o formulário de simulação apresenta mensagem correspondente ao campo:")]
        public void EntaoVejoQueOFormularioDeSimulacaoApresentaMensagem(Table table)
        {
            var expectedMsg = table.CreateInstance<Form>();

            var actualAppliedValue = _parallelConfig.CurrentPage.As<SimulatorInvestmentPage>().GetFieldMsgAppliedValue();
            var actualInvestedValue = _parallelConfig.CurrentPage.As<SimulatorInvestmentPage>().GetFieldInvestedValue();
            var actualPeriod = _parallelConfig.CurrentPage.As<SimulatorInvestmentPage>().GetFieldMsgPeriod();

            Assert.AreEqual(expectedMsg.ValorAplicar, actualAppliedValue);
            Assert.AreEqual(expectedMsg.ValorInvestir, actualInvestedValue);
            Assert.AreEqual(expectedMsg.Tempo, actualPeriod);
        }

        [Given(@"Eu já tenha realizado uma simução")]
        public void DadoEuJaTenhaRealizadoUmaSimucao()
        {
        }

        [Given(@"a página do simulador de investimento foi carregada com sucesso")]
        public void DadoAPaginaDoSimuladorDeInvestimentoFoiCarregadaComSucesso()
        {
            _parallelConfig.Driver.Navigate().GoToUrl(Settings.AUT);
            _parallelConfig.CurrentPage = new SimulatorInvestmentPage(_parallelConfig);
        }
    }
}
