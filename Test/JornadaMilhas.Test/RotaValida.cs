using Bogus;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test;

public class RotaValida
{
    [Fact]
    public void ValidaOrigemNulaOuVaziaDaRota()
    {
        var origem = "";
        var fakerRota = new Faker<Rota>()
            .CustomInstantiator(f =>
            {
                return new(origem, f.Address.City());
            });


        var rotaOrigemVazia = fakerRota.Generate();
        var rotaOrigemNula = fakerRota.Generate();
        rotaOrigemNula.Origem = null;


        Assert.Null(rotaOrigemNula.Origem);
        Assert.Empty(rotaOrigemVazia.Origem);
        Assert.Contains("A rota não pode possuir uma origem nula ou vazia.", rotaOrigemVazia.Erros.Sumario);
        Assert.Contains("A rota não pode possuir uma origem nula ou vazia.", rotaOrigemNula.Erros.Sumario);
    }

    [Fact]
    public void ValidaDestinoNuloOuVazioDaRota()
    {
        string destino = null;
        var fakerRota = new Faker<Rota>()
            .CustomInstantiator(f =>
            {
                return new(f.Address.City(), destino);
            });


        var rotaDestinoNulo = fakerRota.Generate();
        var rotaDestinoVazio = fakerRota.Generate();
        rotaDestinoVazio.Destino = "";


        Assert.Null(rotaDestinoNulo.Destino);
        Assert.Empty(rotaDestinoVazio.Destino);
        Assert.Contains("A rota não pode possuir um destino nulo ou vazio.", rotaDestinoVazio.Erros.Sumario);
        Assert.Contains("A rota não pode possuir um destino nulo ou vazio.", rotaDestinoVazio.Erros.Sumario);
    }
}
