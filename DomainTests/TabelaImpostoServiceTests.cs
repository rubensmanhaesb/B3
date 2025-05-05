using B3.Domain.Services;


namespace DomainTests
{
    public class TabelaImpostoServiceTests
    {
        [Theory]
        [InlineData(3, 0.225)]
        [InlineData(6, 0.225)]
        [InlineData(9, 0.20)]
        [InlineData(15, 0.175)]
        [InlineData(30, 0.15)]
        public void Deve_Retornar_Aliquota_Correta(int prazo, decimal aliquotaEsperada)
        {

            var service = new TabelaImpostoService();


            var aliquota = service.ObterAliquota(prazo);


            Assert.Equal(aliquotaEsperada, aliquota);
        }
    }
}
