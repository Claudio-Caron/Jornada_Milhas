using Microsoft.IdentityModel.Tokens;

public class Mus
{
    private int? anoLancamento = null;
    private string? artista = "Artista Desconhecido";

    public Mus(string nome)
    {
        Nome = nome;
    }


    public string Nome { get; set; }
    public int Id { get; set; }
    public string? Artista 
    {
        get => artista;
        set
        {
            if (value.IsNullOrEmpty())
            {
                artista = "Artista Desconhecido";
            }
            else
            {
                artista = value;
            }

        }
    }
    public int? AnoLancamento
    {
        get => anoLancamento;
        set
        {
            if (value <= 0)
            {
                anoLancamento = null;
            }
            else
            {
                anoLancamento = value;
            }

        }
    }

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");

    }

    public override string ToString()
    {
        return @$"Id: {Id} Nome: {Nome}";
    }
}