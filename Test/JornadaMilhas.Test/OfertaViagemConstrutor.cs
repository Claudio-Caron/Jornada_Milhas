using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test;

public class OfertaViagemConstrutor
{
    [Theory]
    [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
    [InlineData("OrigemTeste", "DestinoTeste", "2024-02-01", "2024-02-05", 100, true)]
    [InlineData(null, "São Paulo", "2024-01-01", "2024-01-02", -1, false)]
    [InlineData("Vitória", "São Paulo", "2024-01-01", "2024-01-01", 0, false)]
    [InlineData("Rio de Janeiro", "São Paulo", "2024-01-01", "2024-01-02", -500, false)]
    //código do teste omitido

    public void ReturnIsValidAccordingToTheInputData(string origem, string destino, string dataInicial,
        string dataFinal, double preco, bool validacao)
    {
        //cenario - arrange
        Rota rota = new(origem, destino);
        Periodo periodo = new(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal));


        //ação (do teste) - action
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //Validação - assert
        Assert.Equal(validacao, oferta.EhValido);

    }
    [Fact]
    public void ReturnRouteErrorMessageOrInvalidPeriodWhenNullRoute()
    {
        Rota rota = null;
        Periodo periodo = new(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
        double preco = 100;
        bool validacao = true;

        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);

    }
    public void ReturnDateErrorMessageWhenFinalDateLessInitialDate()
    {
        Rota rota = new("OrigemTeste", "DestinoTeste");
        Periodo periodo = new(new DateTime(2024, 2, 5), new DateTime(2024, 2, 1));
        double preco = 100;
        bool validacao = true;

        OfertaViagem oferta = new(rota, periodo, preco);

        Assert.Contains("Erro: Data de ida não pode ser maior que a data de volta.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);
    }
    [Fact]
    public void ReturnErrorMessageWhenPrecoLessThanZero()
    {
        Rota rota = new Rota("Origem1", "Destino1");
        Periodo periodo = new Periodo(new DateTime(2025, 8, 20), new DateTime(2025, 8, 30));
        double preco = -250;
        

        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);


        Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
    }
    //public void TestandoNomeMusica()
    //{
    //    string nome = "Back in Black";

    //    var musica = new Mus(nome);

    //    Assert.Equal(nome, musica.Nome);

        

    //}
    //public void TestandoIdentityMusica()
    //{
    //    var random = new Random();
    //    int id = random.Next(30);

    //    var musica = new Mus("Crawling");
    //    musica.Id = id;
         
    //    Assert.Equal(id, musica.Id);

    //}
    //public void TestandoToStringMusica()
    //{
    //    var random = new Random();
    //    int id = random.Next(30);
    //    string nome = "Something in the way";

    //    var musica = new Mus(nome);
    //    musica.Id = id;

    //    Assert.Equal(@$"Id: {id} Nome: {nome}", musica.ToString());

    //}
}