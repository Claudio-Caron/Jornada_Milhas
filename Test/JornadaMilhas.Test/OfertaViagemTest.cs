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

        //a��o (do teste) - action
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //Valida��o - assert
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

        Assert.Contains("A oferta de viagem n�o possui rota ou per�odo v�lidos.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);

    }
}