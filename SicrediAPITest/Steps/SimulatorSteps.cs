using System.Collections.Generic;
using System.Linq;
using AutoSicredi.Base;
using AutoSicredi.Config;
using AutoSicredi.Extensions;
using AutoSicredi.Model;
using AutoSicredi.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SicrediAPITest.Steps
{
    [Binding]
    public sealed class SimulatorSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly RestSettings _settings;

        public SimulatorSteps(ScenarioContext scenarioContext, RestSettings settings)
        {
            _scenarioContext = scenarioContext;
            _settings = settings;
        }

        [Given(@"Eu defini as configurações REST")]
        public void DadoEuDefiniAsConfiguracoesREST()
        {
            _settings.Request = new RestRequest(ApiSettings.AUT, Method.POST);
            _settings.Request.AddHeader("Content-Type", ApiSettings.ContentType);
        }

        [When(@"Eu realizo a operação POST para simular um investimento na poupança:")]
        public void QuandoEuRealizoAOperacaoPOSTParaSimularUmInvestimentoNaPoupanca(Table table)
        {
            var simulation = table.CreateInstance<Simulation>();

            var parameters = simulation.ToString();

            _settings.Request.AddParameter("application/x-www-form-urlencoded", parameters, ParameterType.RequestBody);
            _settings.Response = _settings.RestClient.Execute(_settings.Request);
        }

        [Then(@"será retornado as projeções de rentabilidade (.*) \(Meses\) X R\$ (.*)")]
        public void EntaoDeveraSerRetornadoAsProjecoesDeRentabilidadeMesesXR_(string months, string amounts)
        {
            var listProjection = JsonConvert.DeserializeObject<List<Projection>>(_settings.Response.Content);

            
            var expectedListMonths = DataExtensios.ConvertToList(months);
            var expectedListAmounts = DataExtensios.ConvertToList(amounts);

            var actualListMonths = ConvertListMonths(listProjection);
            var actualListAmounts = ConvertListAmounts(listProjection);

            Assert.IsTrue(expectedListMonths.SequenceEqual(actualListMonths));
            Assert.IsTrue(expectedListAmounts.SequenceEqual(actualListAmounts));
        }

        [Then(@"o status retornado deverá ser (.*)")]
        public void EntaoOStatusRetornadoDeveraSer(int expectedHttpCode)
        {
            var statuCode = _settings.Response.StatusCode;
            int actualHttpCode = (int)statuCode;

            Assert.AreEqual(expectedHttpCode, actualHttpCode);
        }

        [Then(@"deverá ser retornado (.*)")]
        public void EntaoDeveraSerRetornado(string expectedMessage)
        {
            var actualMessage =_settings.Response.Content;
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        private List<string> ConvertListMonths(List<Projection> listProjection)
        {
            List<string> listMonths = new List<string>();

            foreach (var proj in listProjection)
            {
                listMonths.Add(proj.Tempo);
            }
            return listMonths;
        }

        private List<string> ConvertListAmounts(List<Projection> listProjection)
        {
            List<string> listAmounts = new List<string>();

            foreach (var proj in listProjection)
            {
                listAmounts.Add(proj.Valor);
            }
            return listAmounts;
        }
    }
}
