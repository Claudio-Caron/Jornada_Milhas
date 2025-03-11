using JornadaMilhas.Dados;
using JornadaMilhasV1.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace JornadaMilhas.Test;

public class OfertaViagemDalAdicionar:IClassFixture<ContextoFixture>
{
    
    private readonly JornadaMilhasContext context;

    
    public OfertaViagemDalAdicionar(ITestOutputHelper output, ContextoFixture fixture)
    {
        
        context = fixture.Context;
        output.WriteLine(context.GetHashCode().ToString());
    }
    [Fact]
    public void RegistraOfertaNoBanco()
    {
        //arrange
        var oferta = SetupOfertaViagem();
        var dal = new OfertaViagemDAL(context);


        //act
        dal.Adicionar(oferta);


        //assert
        var ofertaIncluida = dal.RecuperarPorId(oferta.Id);
        Assert.NotNull(ofertaIncluida);
        Assert.Equal(ofertaIncluida.Preco, oferta.Preco);
        
        

    }

    [Fact]
    public void RegistraOfertaNoBancoComInformacoesCorretas()
    {
        //arrange



        var oferta = SetupOfertaViagem();
        
        var dal = new OfertaViagemDAL(context);


        //act
        dal.Adicionar(oferta);


        //assert
        var ofertaIncluida = dal.RecuperarPorId(oferta.Id);

        Assert.Equal(ofertaIncluida.Preco, oferta.Preco, 0.001);
        Assert.Equal(ofertaIncluida.Rota.Origem, oferta.Rota.Origem);
        Assert.Equal(ofertaIncluida.Rota.Destino, oferta.Rota.Destino);
        Assert.Equal(ofertaIncluida.Periodo.DataInicial, oferta.Periodo.DataInicial);
        Assert.Equal(ofertaIncluida.Periodo.DataFinal, oferta.Periodo.DataFinal);


    }
    public OfertaViagem SetupOfertaViagem()
    {
        Rota rota = new Rota("São Paulo", "Fortaleza");
        Periodo periodo = new Periodo(new DateTime(2025, 03, 07), new DateTime(2025, 03, 19));
        double preco = 350;

        return new OfertaViagem(rota, periodo, preco);
    }
}
