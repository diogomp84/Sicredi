using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSicredi.Model
{
    public class Simulation
    {
        public string Perfil { get; set; }
        public string ValorAplicado { get; set; }
        public string PoupadoPorMes { get; set; }
        public string PeriodoPoupado { get; set; }
        public string TipoPeriodo { get; set; }

        public Simulation(string perfil, string valorAplicado, string poupadoPorMes, string periodoPoupado, string tipoPeriodo)
        {
            Perfil = perfil;
            ValorAplicado = valorAplicado;
            PoupadoPorMes = poupadoPorMes;
            PeriodoPoupado = periodoPoupado;
            TipoPeriodo = tipoPeriodo;
        }

        public override string ToString()
        {
            return "perfil=" + Perfil + "&valorAplicar=" + ValorAplicado + "&valorInvestir=" + PoupadoPorMes + "&tempo=" + PeriodoPoupado + "&periodo=" + TipoPeriodo;
        }
    }
}
