using JornadaMilhas.Dados;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace JornadaMilhas.Test.Integration
{


    [Collection(nameof(ContextoCollection))]
    public class OfertaViagemDalRecuperarTodas
    {
        public readonly JornadaMilhasContext context;
        public OfertaViagemDalRecuperarTodas(ITestOutputHelper output, ContextoFixture contexto)
        {
            context = contexto.Context;
            output.WriteLine(output.GetHashCode().ToString());
        }
        [Fact]
        public void RecuperaOfertasViagemDoBanco()
        {
            //arrange
            var dal = new OfertaViagemDAL(context);
            
            //act
            var ofertasViagem = dal.RecuperarTodas();

            //assert
            Assert.NotNull(ofertasViagem);

        }
    }
}
