﻿using Bogus;
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
public class OfertaViagemDalRecuperaMaiorDesconto:IDisposable
{
    private readonly JornadaMilhasContext context;
    private readonly ContextoFixture fixture;

    public OfertaViagemDalRecuperaMaiorDesconto(ContextoFixture fixture)
    {
        context = fixture.Context;
        this.fixture = fixture;
    }

    public void Dispose()
    {
        fixture.LimpaDadosDoBancoAsync();
    }

    [Fact]
    //destino = São Paulo, desconto = 40, preco = 80
    public void RetornaOfertaEspecificaQuandoDestinoSaoPauloEDesconto40()
    {
        //arrange
        var rota = new RotaDataBuilder() { Origem = "Campo Grande", Destino= "Belo Horizonte"}.Build();
        var periodo = new PeriodoDataBuilder() { DataInicial = new DateTime(2024, 5, 20) }.Build();


        fixture.CriaDadosFake();
        var ofertaEscolhida = new OfertaViagem(rota, periodo,
            80)
        {
            Desconto = 40,
            Ativa = true
        };


        var dal = new OfertaViagemDAL(context);
        dal.Adicionar(ofertaEscolhida);

        
        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
        double precoEsperado = 40;

        //act
        var oferta = dal.RecuperaMaiorDesconto(filtro);

        //assert
        Assert.NotNull(oferta);
        Assert.Equal(precoEsperado, oferta.Preco, 0.0001);
    }
    [Fact]
    
    public void RetornaOfertaEspecificaQuandoDestinoSaoPauloEDesconto60()
    {
        //arrange
        var rota = new RotaDataBuilder() { Origem = "Campo Grande", Destino = "Belo Horizonte" }.Build();
        var periodo = new PeriodoDataBuilder() { DataInicial = new DateTime(2024, 5, 20) }.Build();


        fixture.CriaDadosFake();
        var ofertaEscolhida = new OfertaViagem(rota, periodo,
            80)
        {
            Desconto = 60,
            Ativa = true
        };


        var dal = new OfertaViagemDAL(context);
        dal.Adicionar(ofertaEscolhida);


        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
        double precoEsperado = 20;

        //act
        var oferta = dal.RecuperaMaiorDesconto(filtro);

        //assert
        Assert.NotNull(oferta);
        Assert.Equal(precoEsperado, oferta.Preco, 0.0001);
    }
}
