using Bogus;
using JornadaMilhas.Dados;
using JornadaMilhasV1.Gerencidor;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test.Integration;

[Collection(nameof(ContextoCollection))]
public class OfertaViagemDalRecuperaMaiorDesconto
{
    private readonly JornadaMilhasContext context;
    public OfertaViagemDalRecuperaMaiorDesconto(ContextoFixture fixture)
    {
        context = fixture.Context;
    }

    [Fact]
    //destino = São Paulo, desconto = 40, preco = 80
    public void RetornaOfertaEspecificaQuandoDestinoSaoPauloEDesconto40()
    {
        //arrange
        var fakerPeriodo = new Faker<Periodo>().CustomInstantiator
            (f =>
            {
                DateTime dataInicial = f.Date.Soon();
                return new Periodo(dataInicial, dataInicial.AddDays(30));
            });


        var rota = new Rota("Curitiba", "São Paulo");

        var fakerOferta = new Faker<OfertaViagem>()
            .CustomInstantiator(f =>
            {
                return new OfertaViagem(rota, fakerPeriodo.Generate(), 100 * f.Random.Int(1, 100));
            })
            .RuleFor(o => o.Desconto, f => 40)
            .RuleFor(o => o.Ativa, f => true);

        var ofertaEscolhida = new OfertaViagem(rota, fakerPeriodo.Generate(),
            80)
        {
            Desconto = 40,
            Ativa = true
        };

        var ofertaInativa = new OfertaViagem(rota, fakerPeriodo, 70)
        {
            Desconto = 40,
            Ativa = false
        };

        var dal = new OfertaViagemDAL(context);

        var lista = fakerOferta.Generate(200);
        lista.Add(ofertaEscolhida);
        lista.Add(ofertaInativa);

        
        context.OfertasViagem.AddRange(lista);
        context.SaveChanges();
        
        
        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
        double precoEsperado = 40;

        //act
        var oferta = dal.RecuperaMaiorDesconto(filtro);

        //assert
        Assert.NotNull(oferta);
        Assert.Equal(precoEsperado, oferta.Preco, 0.0001);
    }
}
