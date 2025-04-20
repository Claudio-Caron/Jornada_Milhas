using Bogus;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test.Integration;

public class PeriodoDataBuilder:Faker<Periodo>
{
    public DateTime? DataInicial { get; set; }
    public DateTime? DataFinal { get; set; }
    public PeriodoDataBuilder()
    {
        CustomInstantiator(f =>
        {
            DateTime dataInicial = DataInicial ?? f.Date.Soon();
            DateTime dataFinal = DataFinal ?? dataInicial.AddDays(30);
            return new Periodo(dataInicial, dataFinal);
        });
    }
    public Periodo Build() => Generate();
} 


