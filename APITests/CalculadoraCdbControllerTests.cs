using B3.API.Models;
using B3.Application.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;


namespace APITests
{
    public class CalculadoraCdbControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();


        [Fact]
        public async Task Post_Calcular_Deve_Retornar_Resultado_Valido()
        {
            var request = new CdbRequest
            {
                ValorInicial = 1000,
                PrazoEmMeses = 6
            };

            var response = await _client.PostAsJsonAsync("/api/CalculadoraCdb", request);
            response.EnsureSuccessStatusCode();

            var resultado = await response.Content.ReadFromJsonAsync<CdbResultDto>();
            resultado.Should().NotBeNull();
            resultado!.ValorBruto.Should().BeGreaterThan(34);
            resultado.ValorLiquido.Should().BeLessThan(resultado.ValorBruto);
        }

        [Fact]
        public async Task Post_Calcular_Deve_Retornar_BadRequest_Para_Entrada_Invalida()
        {
            var request = new CdbRequest
            {
                ValorInicial = 0,
                PrazoEmMeses = 1
            };

            var response = await _client.PostAsJsonAsync("/api/CalculadoraCdb", request);
            var body = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            body.Should().Contain("Valor inicial deve ser maior que zero");
            body.Should().Contain("Prazo deve ser maior que 1 mês");
        }

        [Fact]
        public void ErroResponse_Deve_Ser_Instanciado_Corretamente()
        {
            var erro = new ErroResponse
            {
                Status = 400,
                Mensagem = "Erro",
                Detalhe = "Detalhe do erro"
            };

            erro.Status.Should().Be(400);
            erro.Mensagem.Should().Be("Erro");
            erro.Detalhe.Should().Be("Detalhe do erro");
        }

        [Fact]
        public async Task Post_Calcular_Deve_Lancar_Excecao_Se_Request_Nulo()
        {
            var content = new StringContent("null", System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/CalculadoraCdb", content);
            //var body = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }


    }
}
