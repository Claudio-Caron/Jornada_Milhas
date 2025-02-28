using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test;

public class OfertaViagemTest
{
    [Fact]
    public void TestantoOfertaValida()
    {
        //cenario - arrange
        Rota rota = new("OrigemTeste", "DestinoTeste");
        Periodo periodo = new(new DateTime(2024, 2, 1), new DateTime(2024,2,5));
        double preco = 100;
        bool validacao = true;

        //ação (do teste) - action
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //Validação - assert
        Assert.Equal(validacao, oferta.EhValido);

    }
    [Fact]
    public void TestandoOfertaComRotaNula()
    {
        Rota rota = null;
        Periodo periodo = new(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
        double preco = 100;
        bool validacao = true;

        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);

    }
}