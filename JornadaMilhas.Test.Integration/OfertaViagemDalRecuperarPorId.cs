using JornadaMilhas.Dados;
using System;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace JornadaMilhas.Test.Integration;

[Collection(nameof(ContextoCollection))]
public class OfertaViagemDalRecuperarPorId
{

    public readonly JornadaMilhasContext context;
    public OfertaViagemDalRecuperarPorId(ITestOutputHelper output, ContextoFixture fixture)
    {
        context  = fixture.Context;
        output.WriteLine(context.GetHashCode().ToString());
    }
    [Fact]
    public void RetornaNuloQuandoIdInexistente()
    {
        //arrange
        var dal = new OfertaViagemDAL(context);

        //act

        var ofertaRecuperada = dal.RecuperarPorId(-2);
        //assert
        Assert.Null(ofertaRecuperada);
    }
}
