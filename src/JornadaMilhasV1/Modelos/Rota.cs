using JornadaMilhasV1.Validador;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhasV1.Modelos;

public class Rota: Valida
{
    private string origem;
    private string destino;

    public int Id { get; set; }
    public string Origem { 
        get => origem;
        set
        {
            origem = value;
            Validar();
        }
    }
    public string Destino
    {
        get => destino;
        set
        {
            destino = value;
            Validar();
        }
    }

    public Rota(string origem, string destino)
    {
        Origem = origem;
        Destino = destino;
        Validar();
    }

    protected override void Validar()
    {
        if ((this.Origem is null) || this.Origem.Equals(string.Empty))
        {
            Erros.RegistrarErro("A rota não pode possuir uma origem nula ou vazia.");
        }
        if ((this.Destino is null) || this.Destino.Equals(string.Empty))
        {
            Erros.RegistrarErro("A rota não pode possuir um destino nulo ou vazio.");
        }
    }
}
