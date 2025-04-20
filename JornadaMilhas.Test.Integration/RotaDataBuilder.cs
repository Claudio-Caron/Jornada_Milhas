using Bogus;
using JornadaMilhasV1.Modelos;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test.Integration;

public class RotaDataBuilder:Faker<Rota>
{
    public string Origem {  get; set; }
    public string Destino { get; set; }
    public RotaDataBuilder()
    {
        CustomInstantiator(f =>
        {
            string origem = Origem ?? f.Name.Locale;
            string destino = Destino ?? f.Name.Locale;
            return new Rota(origem, destino);
        });
    }
    public Rota Build() => Generate();
}
